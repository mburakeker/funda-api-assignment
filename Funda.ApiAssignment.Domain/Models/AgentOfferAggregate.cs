namespace Funda.ApiAssignment.Domain.Models;

public class AgentOfferAggregate
{
    public required int RealEstateAgentId { get; init; }
    public required string RealEstateAgentName { get; init; }
    public required int OfferCount { get; init; }
}