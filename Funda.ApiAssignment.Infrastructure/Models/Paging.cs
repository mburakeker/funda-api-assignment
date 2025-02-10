using System.Text.Json.Serialization;

namespace Funda.ApiAssignment.Infrastructure.Models;

public class Paging
{
    [JsonPropertyName("AantalPaginas")]
    public int PageCount { get; init; }
}