namespace Funda.ApiAssignment.Infrastructure.Models;

public class SearchResult
{
    public required Property[] Objects { get; init; }
    public required Paging Paging { get; init; }
}