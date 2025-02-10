using Funda.ApiAssignment.Domain.Models;

namespace Funda.ApiAssignment.Domain.Handlers;

public interface IAgentReportHandler
{
    public Task<AgentOfferAggregate[]?> GetTop10AgentsByOfferCount(OfferType offerType, string searchQuery);
}