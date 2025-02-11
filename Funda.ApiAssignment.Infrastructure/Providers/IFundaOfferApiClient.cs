using Funda.ApiAssignment.Infrastructure.Models;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public interface IFundaOfferApiClient
{
    public Task<SearchResult?> GetOffers(string offerType, string searchQuery, int pageId, int pageSize);
}