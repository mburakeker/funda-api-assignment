namespace Funda.ApiAssignment.Domain.Models;

public class Offer
{
    public Guid Id { get; init; }
    public int AgentId { get; init; }
    public required string AgentName { get; init; }
}