using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Domain.Providers;
using Funda.ApiAssignment.Infrastructure.Mappers;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public class FundaOfferApiProvider(
    IFundaOfferApiClient fundaOfferApiClient)
    : IFundaOfferApiProvider
{
    private const int PageSize = 25;
    public async Task<IEnumerable<Offer>?> GetAllOffers(OfferType offerType, string searchQuery)
    {
        var offerTypeString = OfferTypeMapper.MapOfferTypeToApiString(offerType);
        
        // Fetch the first page to get the total number of pages
        var initialSearch = await fundaOfferApiClient.GetOffers(offerTypeString, searchQuery, 1, 1);
        if (initialSearch == null || initialSearch.Paging.PageCount == 0 || initialSearch.Objects.Length == 0)
        {
            return null;
        }

        // Create tasks to fetch all pages
        var numberOfTasks = (int) Math.Ceiling((double) initialSearch.TotalObjectCount / PageSize);
        var tasks = Enumerable.Range(1, numberOfTasks)
            .Select(i => fundaOfferApiClient.GetOffers(offerTypeString, searchQuery, i, PageSize))
            .ToList();
        await Task.WhenAll(tasks);
        
        // Combine all offers
        var allOffers = tasks
            .SelectMany(t => t.Result?.Objects ?? [])
            .Select(o => new Offer
            {
                Id = o.Id,
                AgentId = o.AgentId,
                AgentName = o.AgentName,
            });

        return allOffers;
    }
}