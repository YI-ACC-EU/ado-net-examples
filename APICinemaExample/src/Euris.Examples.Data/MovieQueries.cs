namespace Euris.Examples.Data;

public static class MovieQueries
{
    public static string GetMovieQuery() => $@"
  SELECT { string.Join(',', MovieFields.AllMovieFields) }
  FROM [movies].[dbo].[movie] M
  WHERE M.[movie_id] = @Id'; 
";

    public static string GetGenreByMovieIdQuery() => @"
SELECT g.genre_id as [Id], g.genre_name as [Name] FROM 
[dbo].[movie_genres] mg
INNER JOIN [dbo].[genre] g ON mg.genre_id = g.genre_id
WHERE mg.movie_id = @MovieId;
";

    public static string GetActorsByMovieIdQuery() => @"
SELECT p.person_id as [Id], c.character_name as [CharacterName], g.gender as [Gender], p.person_name as [Name] FROM 
[dbo].[movie_cast] c
INNER JOIN [dbo].[person] p ON c.person_id = p.person_id
LEFT JOIN [dbo].[gender] g ON c.gender_id = g.gender_id
WHERE c.movie_id = @MovieId
ORDER BY c.cast_order ASC;
";

    public static string GetCrewByMovieIdQuery() => @"
SELECT p.person_id as [Id], c.job as [Job], p.person_name as [Name] FROM 
[dbo].[movie_crew] c
INNER JOIN [dbo].[person] p ON c.person_id = p.person_id
WHERE c.movie_id = @MovieId;
";

    public static string GetCompanyByMovieIdQuery() => @"
SELECT pc.company_id as Id, pc.company_name as [Name] FROM 
[dbo].[movie_company] mc
INNER JOIN [dbo].[production_company] pc ON mc.company_id = pc.company_id
WHERE mc.movie_id = @MovieId;
";

    public static string GetCountryByMovieIdQuery() => @"
SELECT c.country_id as Id, c.country_iso_code as IsoCode, c.country_name as [Name] FROM
[dbo].[production_country] pc
INNER JOIN [dbo].[country] c ON pc.country_id = c.country_id
WHERE pc.movie_id = @MovieId;
";

    public static string MovieSearchQueryBase() => $@"
SELECT *, COUNT(*) over() as TotalCount FROM (
SELECT DISTINCT { string.Join(',', MovieFields.AllMovieFields) }
  FROM [movies].[dbo].[movie] m
   /****JOIN******/
   /****WHERE*****/  
  ) as T
  ORDER BY title 
  OFFSET @Offest ROWS
  FETCH NEXT @Take ROWS ONLY;
";

}

public static class MovieFields
{
    public const string MovieId = "movie_id";
    public const string Title = "title";
    public const string Budget = "budget";
    public const string HomePage = "homepage";
    public const string MovieStatus = "movie_status";
    public const string Overview = "overview";
    public const string Popularity = "popularity";
    public const string ReleaseDate = "release_date";
    public const string Revenue = "revenue";
    public const string Runtime = "runtime";
    public const string TagLine = "tagline";
    public const string VoteAverage = "vote_average";
    public const string VoteCount = "vote_count";
    public const string Id = "Id";
    public const string Name = "Name";
    public const string Gender = "Gender";
    public const string CharacterName = "CharacterName";
    public const string Job = "Job";
    public const string IsoCountryCode = "IsoCode";

    public static readonly string[] AllMovieFields = new[]
    {
        MovieId, Title, Budget, HomePage, MovieStatus, Overview, Popularity, 
        ReleaseDate, Revenue, Runtime, TagLine, VoteAverage, VoteCount 
    };
}