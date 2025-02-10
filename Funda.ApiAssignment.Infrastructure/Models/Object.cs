using System.Text.Json.Serialization;

namespace Funda.ApiAssignment.Infrastructure.Models;

public class Object
{
    public Guid Id { get; init; }
    [JsonPropertyName("MakelaarId")]
    public int AgentId { get; init; }
    [JsonPropertyName("MakelaarNaam")]
    public required string AgentName { get; init; }
}