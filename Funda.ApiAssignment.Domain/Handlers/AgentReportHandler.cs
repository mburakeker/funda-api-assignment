using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Domain.Providers;

namespace Funda.ApiAssignment.Domain.Handlers;

public class AgentReportHandler(IFundaOfferApiProvider fundaOfferApiProvider) : IAgentReportHandler
{
    public async Task<AgentOfferAggregate[]?> GetTop10AgentsByOfferCount(OfferType offerType, string searchQuery)
    {
        var allOffers = await fundaOfferApiProvider.GetAllOffers(offerType, searchQuery);

        return allOffers?.GroupBy(x => x.AgentId)
            .Select(g => new AgentOfferAggregate
            {
                RealEstateAgentId = g.Key,
                RealEstateAgentName = g.First().AgentName,
                OfferCount = g.Select(x => x.Id).Distinct().Count()
            })
            .OrderByDescending(x => x.OfferCount)
            .Take(10)
            .ToArray();
    }
}