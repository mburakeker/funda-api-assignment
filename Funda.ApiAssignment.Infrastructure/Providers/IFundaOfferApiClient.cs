using Funda.ApiAssignment.Infrastructure.Models;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public interface IFundaOfferApiClient
{
    public Task<GetOffersResponse?> GetOffers(string offerType, string? searchQuery, int? pageId, int? pageSize);
}