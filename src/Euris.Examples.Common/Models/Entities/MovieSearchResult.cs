namespace Euris.Examples.Common.Models.Entities;

public class MovieSearchResult
{
    public List<Movie> Movies { get; set; }
    public int TotalResults { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; } = 10;
}