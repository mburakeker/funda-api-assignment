using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Domain.Providers;

namespace Funda.ApiAssignment.Domain.Handlers;

public class AgentReportHandler(IFundaOfferApiProvider fundaOfferApiProvider) : IAgentReportHandler
{
    public async Task<AgentOfferAggregate[]?> GetTop10AgentsByOfferCount(OfferType offerType, string searchQuery)
    {
        var allOffers = await fundaOfferApiProvider.GetAllOffers(offerType, searchQuery);

        var agentsByOfferCount = from offer in allOffers
            group offer by offer.AgentId
            into agentGroup
            select new AgentOfferAggregate
            {
                AgentId = agentGroup.Key,
                AgentName = agentGroup.First().AgentName,
                OfferCount = agentGroup.Count()
            };

        return agentsByOfferCount
            .OrderByDescending(x => x.OfferCount)
            .Take(10)
            .ToArray();
    }
}