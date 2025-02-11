using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Domain.Providers;
using Funda.ApiAssignment.Infrastructure.Mappers;
using Funda.ApiAssignment.Infrastructure.Models;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public class FundaOfferApiProvider(
    IFundaOfferApiClient fundaOfferApiClient)
    : IFundaOfferApiProvider
{
    private const int PageSize = 25;
    public async Task<IEnumerable<Offer>?> GetAllOffers(OfferType offerType, string searchQuery)
    {
        // Map the offer type to the API string
        var offerTypeString = OfferTypeMapper.MapOfferTypeToApiString(offerType);
        
        // Fetch the first page to get the total number of pages
        var initialSearchResult = await fundaOfferApiClient.GetOffers(offerTypeString, searchQuery, 1, PageSize);
        if (initialSearchResult == null || initialSearchResult.Objects.Length == 0)
        {
            return null;
        }

        // Fetch all pages starting from the second page to reuse the first page
        var searchResults = new List<SearchResult> { initialSearchResult };
        for (var i = 2; i <= initialSearchResult.Paging.AantalPaginas; i++)
        {
            var page = await fundaOfferApiClient.GetOffers(offerTypeString, searchQuery, i, PageSize);
            if (page == null || page.Objects.Length == 0)
            {
                break;
            }

            searchResults.Add(page);
        }
        
        // Combine all offers into a single collection
        var allOffers = searchResults.SelectMany(r => r.Objects);

        // Map the offers to the domain model
        return allOffers.Select(o => new Offer
        {
            Id = o.Id,
            AgentId = o.MakelaarId,
            AgentName = o.MakelaarNaam,
        });
    }
}