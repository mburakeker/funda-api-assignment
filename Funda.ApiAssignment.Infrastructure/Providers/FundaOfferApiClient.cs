using System.Net.Http.Json;
using Funda.ApiAssignment.Infrastructure.Models;
using Funda.ApiAssignment.Infrastructure.Setup;
using Microsoft.Extensions.Options;

namespace Funda.ApiAssignment.Infrastructure.Providers;

public class FundaOfferApiClient(HttpClient httpClient,
    IOptions<FundaOfferApiSettings> fundaOfferApiSettings) : IFundaOfferApiClient
{
    private readonly string _apiKey = fundaOfferApiSettings.Value.ApiKey;
    public async Task<SearchResult?> GetOffers(string offerType, string searchQuery, int pageId, int pageSize)
    {
        var response = await httpClient.GetAsync(
                $"{_apiKey}/?type={offerType}&zo={searchQuery}&page={pageId}&pagesize={pageSize}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<SearchResult>();
    }
}