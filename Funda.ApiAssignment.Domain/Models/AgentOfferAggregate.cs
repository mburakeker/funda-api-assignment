namespace Funda.ApiAssignment.Domain.Models;

public class AgentOfferAggregate
{
    public required int AgentId { get; init; }
    public required string AgentName { get; init; }
    public required int OfferCount { get; init; }
}