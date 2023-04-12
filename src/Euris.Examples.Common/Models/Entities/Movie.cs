namespace Euris.Examples.Common.Models.Entities;

public class Movie : CommonEntity<int>
{
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
    public List<Actor>? Actors { get; set; }
    public List<CrewMember>? Crew { get; set; }
    public List<Company>? Companies { get; set; }
    public List<Genre>? Genres { get; set; }
    public List<Country>? ProductionCountries { get; set; }

}