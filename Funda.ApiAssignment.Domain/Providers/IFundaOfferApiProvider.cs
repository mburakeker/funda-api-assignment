using Funda.ApiAssignment.Domain.Models;

namespace Funda.ApiAssignment.Domain.Providers;

public interface IFundaOfferApiProvider
{
    public Task<IEnumerable<Offer>?> GetAllOffers(OfferType offerType, string searchQuery);
}