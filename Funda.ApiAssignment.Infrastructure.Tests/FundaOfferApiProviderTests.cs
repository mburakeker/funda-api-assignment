using AutoFixture;
using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Infrastructure.Mappers;
using Funda.ApiAssignment.Infrastructure.Models;
using Funda.ApiAssignment.Infrastructure.Providers;
using NSubstitute;

namespace Funda.ApiAssignment.Infrastructure.Tests;

public class FundaOfferApiProviderTests
{
    private readonly IFixture _fixture;
    private readonly IFundaOfferApiClient _fundaOfferApiClient;
    private readonly FundaOfferApiProvider _fundaOfferApiProvider;
    public FundaOfferApiProviderTests()
    {
        _fixture = new Fixture();
        _fundaOfferApiClient = Substitute.For<IFundaOfferApiClient>();
        _fundaOfferApiProvider = new FundaOfferApiProvider(_fundaOfferApiClient);
    }
    
    [Fact]
    public async Task GetAllOffers_Calls_GetOffers_With_Correct_Parameters()
    {
        // Arrange
        var offerType = OfferType.ForSale;
        var offerTypeString = OfferTypeMapper.MapOfferTypeToApiString(offerType);
        var searchQuery = "/Amsterdam/";
        var pageId = 1;
        var pageSize = 25;

        _fundaOfferApiClient
            .GetOffers(offerTypeString, searchQuery, pageId, pageSize)
            .Returns(new SearchResult
            {
                Objects = [],
                Paging = new Paging
                {
                    AantalPaginas = 1
                }
            });

        // Act
        await _fundaOfferApiProvider.GetAllOffers(offerType, searchQuery);

        // Assert
        await _fundaOfferApiClient.Received().GetOffers(offerTypeString, searchQuery, pageId, pageSize);
    }
    
    [Fact]
    public async Task GetAllOffers_Calls_Api_Based_On_Page_Count()
    {
        // Arrange
        var amountOfPages = 100;
        var searchResult = _fixture.Build<SearchResult>()
            .With(x => x.Paging, new Paging
            {
                AantalPaginas = amountOfPages
            })
            .Create();

        _fundaOfferApiClient
            .GetOffers(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
            .Returns(searchResult);

        // Act
        await _fundaOfferApiProvider.GetAllOffers(_fixture.Create<OfferType>(), _fixture.Create<string>());

        // Assert
        await _fundaOfferApiClient.Received(amountOfPages).GetOffers(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
    }
}