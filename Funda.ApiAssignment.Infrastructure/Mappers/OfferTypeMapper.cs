using Funda.ApiAssignment.Domain.Models;

namespace Funda.ApiAssignment.Infrastructure.Mappers;

public static class OfferTypeMapper
{
    public static string MapOfferTypeToApiString(OfferType offerType)
    {
        return offerType switch
        {
            OfferType.ForSale => "koop",
            OfferType.Rental => "huur",
            _ => throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null)
        };
    }
}