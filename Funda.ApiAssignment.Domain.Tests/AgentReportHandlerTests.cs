using AutoFixture;
using FluentAssertions;
using Funda.ApiAssignment.Domain.Handlers;
using Funda.ApiAssignment.Domain.Models;
using Funda.ApiAssignment.Domain.Providers;
using NSubstitute;

namespace Funda.ApiAssignment.Domain.Tests;

public class AgentReportHandlerTests
{
    private readonly AgentReportHandler _sut;
    private readonly IFundaOfferApiProvider _fundaOfferApiProvider;
    private readonly Fixture _fixture = new();
    public AgentReportHandlerTests()
    {
        _fundaOfferApiProvider = Substitute.For<IFundaOfferApiProvider>();
        _sut = new AgentReportHandler(_fundaOfferApiProvider);
    }
    
    [Fact]
    public async Task GetTop10AgentsByOfferCount_Returns_Aggregate_Correctly()
    {
        // Arrange
        var offerType = _fixture.Create<OfferType>();
        var searchQuery = _fixture.Create<string>();
        var aggregateList = _fixture.CreateMany<AgentOfferAggregate>().ToList();
        var offers = aggregateList.SelectMany(aggregate => _fixture.Build<Offer>()
            .With(o => o.AgentId, aggregate.AgentId)
            .With(o => o.AgentName, aggregate.AgentName)
            .CreateMany(aggregate.OfferCount)
        );
        _fundaOfferApiProvider.GetAllOffers(offerType, searchQuery).Returns(offers);
        
        // Act
        var result = await _sut.GetTop10AgentsByOfferCount(offerType, searchQuery);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(aggregateList.Count);
        
        var topAgent = aggregateList.MaxBy(a => a.OfferCount);
        topAgent.Should().NotBeNull();
        
        result[0].AgentId.Should().Be(topAgent.AgentId); 
        result[0].AgentName.Should().Be(topAgent.AgentName);
        result[0].OfferCount.Should().Be(topAgent.OfferCount);
    }
}