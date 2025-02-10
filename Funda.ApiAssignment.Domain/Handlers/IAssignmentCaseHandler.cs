using Funda.ApiAssignment.Domain.Models;

namespace Funda.ApiAssignment.Domain.Handlers;

public interface IAssignmentCaseHandler
{
    public Task<AgentOfferAggregate[]?> GetTop10AgentsInAmsterdamForSale();
    public Task<AgentOfferAggregate[]?> GetTop10AgentsInAmsterdamWithGardenForSale();
    public Task<AgentOfferAggregate[]?> GetTop10AgentsInBussumWithGardenForSale();
}