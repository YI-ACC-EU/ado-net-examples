namespace Euris.Examples.Common.Models.Requests;

public class MoviesByFilterRequest
{
    public string? FreeText { get; set; }
    public long? BudgetMin { get; set; }
    public long? BudgetMax { get;set; }
    public string? ActorPersonName { get; set; }
    public string? CrewPersonName { get; set; }
    public string? GenreName { get; set; }
    public int PageNumber { get; set; } = 1;
}