namespace Euris.Examples.Data.QueryBuilders;

public class MoviesByFilterQueryBuilder
{
    private const string JoinPlaceholder = "/***JOIN***/";
    private const string WherePlaceholder = "/***WHERE***/";
    private const string WhereWord = "\r\n WHERE \r\n";

    private const string MainQuery = $@"
SELECT *, COUNT(*) over() as TotalCount FROM (
SELECT DISTINCT m.*
  FROM [movies].[dbo].[movie] m
  {JoinPlaceholder}
  {WherePlaceholder}
  ) as T
  ORDER BY title 
  OFFSET {OffsetParameterName} ROWS
  FETCH NEXT {TakeParameterName} ROWS ONLY;
";

    private const string JoinActors = @"
  INNER JOIN [dbo].[movie_cast] mcast ON m.movie_id = mcast.movie_id
  INNER JOIN [dbo].[person] a ON mcast.person_id = a.person_id
";
    
    private const string JoinCrew = @"
  INNER JOIN [dbo].[movie_crew] mcr ON m.movie_id = mcr.movie_id
  INNER JOIN [dbo].[person] c ON mcr.person_id = c.person_id
";

    private const string JoinGenre = @"
  INNER JOIN [dbo].[movie_genres] mg ON m.movie_id = mg.movie_id
  INNER JOIN [dbo].[genre] g ON mg.genre_id = g.genre_id
";

    private const string ActorParameterName = "@ActorPersonName";
    private const string CrewParameterName = "@CrewPersonName";
    private const string GenreParameterName = "@GenreName";
    private const string FreeTextParameterName = "@FreeText";
    private const string BudgetMaxParameterName = "@BudgetMax";
    private const string BudgetMinParameterName = "@BudgetMin";
    private const string OffsetParameterName = "@Offest";
    private const string TakeParameterName = "@Take";

    private readonly string WhereActors = $@" a.person_name LIKE '%' + {ActorParameterName} + '%' ";
    private readonly string WhereCrew = $@" c.person_name LIKE '%' + {CrewParameterName} + '%' ";
    private readonly string WhereGenre = $@" g.genre_name = {GenreParameterName} ";
    private readonly string WhereFreeText =
        $@" (FREETEXT([title], {FreeTextParameterName}) OR FREETEXT([overview], {FreeTextParameterName}) OR FREETEXT([tagline], {FreeTextParameterName})) ";

    private readonly string WhereBudgetGreater = $" m.budget > {BudgetMinParameterName} ";
    private readonly string WhereBudgetLower = $" m.budget < {BudgetMaxParameterName} ";
    private readonly string WhereBudgetBetween = $" m.budget BETWEEN {BudgetMinParameterName} AND {BudgetMaxParameterName} ";

    private List<string> Joins { get; set; }
    private List<string> Wheres { get; set; }
    private Dictionary<string, object> QueryParameters { get; set; }
    private QueryBuilderResult Result { get; set; }
    private int Offset;
    private int Take = 10;

    private MoviesByFilterQueryBuilder()
    {
        Joins = new List<string>();
        Wheres = new List<string>();
        QueryParameters = new Dictionary<string, object>();
    }
    public static MoviesByFilterQueryBuilder Create() => new ();

    public MoviesByFilterQueryBuilder WithFreeText(string? freeText)
    {
        if (string.IsNullOrWhiteSpace(freeText)) return this;
        Wheres.Add(WhereFreeText);
        QueryParameters.Add(FreeTextParameterName, freeText);
        return this;
    }

    public MoviesByFilterQueryBuilder WithActorName(string? actorName)
    {
        if (string.IsNullOrWhiteSpace(actorName)) return this;
        Joins.Add(JoinActors);
        Wheres.Add(WhereActors);
        QueryParameters.Add(ActorParameterName, actorName);
        return this;
    }

    public MoviesByFilterQueryBuilder WithCrewMemberName(string? crewMemberName)
    {
        if (string.IsNullOrWhiteSpace(crewMemberName)) return this;
        Joins.Add(JoinCrew);
        Wheres.Add(WhereCrew);
        QueryParameters.Add(CrewParameterName, crewMemberName);
        return this;
    }

    public MoviesByFilterQueryBuilder WithBudget(long? from, long? to)
    {
        if (from is not null && to is not null)
        {
            Wheres.Add(WhereBudgetBetween);
            QueryParameters.Add(BudgetMinParameterName, from);
            QueryParameters.Add(BudgetMaxParameterName, to);
        }else if (from is not null)
        {
            Wheres.Add(WhereBudgetGreater);
            QueryParameters.Add(BudgetMinParameterName, from);
        }else if (to is not null)
        {
            Wheres.Add(WhereBudgetLower);
            QueryParameters.Add(BudgetMaxParameterName, to);
        }
        return this;
    }

    public MoviesByFilterQueryBuilder WithGenreName(string? genreName)
    {
        if (string.IsNullOrWhiteSpace(genreName)) return this;
        Joins.Add(JoinGenre);
        Wheres.Add(WhereGenre);
        QueryParameters.Add(GenreParameterName, genreName);
        return this;
    }

    public MoviesByFilterQueryBuilder WithPagination(int? pageNumber, int? pageSize)
    {
        var page = pageNumber ?? 0;
        if(pageSize > 0)
            Take = pageSize > 100 ? 100 : pageSize.Value;
        if(pageNumber is not null )
            Offset =  page * Take;
        return this;
    }

    public QueryBuilderResult Build()
    {
        var joinsString = string.Empty;
        var wheresString = string.Empty;
        if (Joins.Any())
            joinsString = string.Join(Environment.NewLine, Joins);
        if (Wheres.Any())
            wheresString = $"{WhereWord} {string.Join($"{Environment.NewLine} AND", Wheres)}";
        QueryParameters.Add(OffsetParameterName, Offset);
        QueryParameters.Add(TakeParameterName, Take);
        return new QueryBuilderResult()
        {
            Query = MainQuery
                .Replace(JoinPlaceholder, joinsString)
                .Replace(WherePlaceholder, wheresString),
            QueryParameters = QueryParameters
        };
    }
}
