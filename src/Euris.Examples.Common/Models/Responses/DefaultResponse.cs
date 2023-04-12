namespace Euris.Examples.Common.Models.Responses;

public class DefaultResponse<T>
{
    public int StatusCode { get; set;}
    public T? Data { get; set; }
    public string[]? Errors { get; set; }
}