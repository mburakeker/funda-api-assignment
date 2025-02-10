using System.Net.Http.Json;
using System.Text;
using Funda.ApiAssignment.Infrastructure.Models;
using Funda.ApiAssignment.Infrastructure.Setup;
using Microsoft.Extensions.Options;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public class FundaOfferApiClient(HttpClient httpClient,
    IOptions<FundaOfferApiSettings> fundaOfferApiSettings) : IFundaOfferApiClient
{
    private readonly string _apiKey = fundaOfferApiSettings.Value.ApiKey;
    public async Task<GetOffersResponse?> GetOffers(string offerType, string? searchQuery, int? pageId, int? pageSize)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"/feeds/Aanbod.svc/json/{Uri.EscapeDataString(_apiKey)}/");
        stringBuilder.Append("?type=");
        stringBuilder.Append(Uri.EscapeDataString(offerType));
        if (!string.IsNullOrEmpty(searchQuery))
        {
            stringBuilder.Append("&zo=");
            stringBuilder.Append(Uri.EscapeDataString(searchQuery));
        }
        
        if (pageId.HasValue)
        {
            stringBuilder.Append("&page=");
            stringBuilder.Append(pageId.Value);
        }
        
        if (pageSize.HasValue)
        {
            stringBuilder.Append("&pagesize=");
            stringBuilder.Append(pageSize.Value);
        }
        
        var response = await httpClient.GetAsync(new Uri(
            stringBuilder.ToString(),
            UriKind.Relative
        ));
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<GetOffersResponse>();
    }
}