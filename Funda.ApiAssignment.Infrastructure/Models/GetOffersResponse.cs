using System.Text.Json.Serialization;

namespace Funda.ApiAssignment.Infrastructure.Models;

public class GetOffersResponse
{
    public required Object[] Objects { get; init; }
    public required Paging Paging { get; init; }
    [JsonPropertyName("TotaalAantalObjecten")]
    public int TotalObjectCount { get; init; }
}