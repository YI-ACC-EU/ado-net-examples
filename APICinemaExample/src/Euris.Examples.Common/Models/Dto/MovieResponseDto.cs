namespace Euris.Examples.Common.Models.Dto;


public class MovieDtoCommon
{
    public string Title { get; set; }
    public long? Budget { get; set; }
    public long? Revenue { get; set; }
    public string? Overview { get; set; }
    public decimal? Popularity { get; set; }
    public string? ReleaseDate { get; set; }
    public int? RunTime { get; set; }
    public string? Status { get; set; }
    public string? Tagline { get; set; }
    public decimal? VoteAverage { get; set; }
    public int? VoteCount { get; set; }
    public string? HomePage { get; set; }
}

public class MovieSearchResponseDto
{
    public List<MovieDtoCommon> Movies { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}


public class DefaultMovieResponseDtoCommon : MovieDtoCommon
{

    public List<ActorDto>? Actors { get; set; }
    public List<CrewMemberDto>? Crew { get; set; }
    public List<string>? Companies { get; set; }
    public List<string>? Genres { get; set; }
    public List<CountryDto>? ProductionCountries { get; set; }
}

public class PersonDto
{
    public string? Name { get; set; }
}

public class ActorDto : PersonDto
{
    public string? Character { get; set; }
    public string? Gender { get; set; }
}

public class CrewMemberDto : PersonDto
{
    public string? Job { get; set; }
}

public class CountryDto
{
    public string? Name { get; set; }
    public string? IsCode { get; set; }
}