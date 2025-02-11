using System.Net;
using Funda.ApiAssignment.Domain.Handlers;
using Funda.ApiAssignment.Domain.Providers;
using Funda.ApiAssignment.Infrastructure.Providers;
using Funda.ApiAssignment.Infrastructure.Setup;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;
using Polly;

namespace Funda.ApiAssignment.API.Setup;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddOptions<FundaOfferApiSettings>()
            .BindConfiguration(FundaOfferApiSettings.SettingName)
            .ValidateDataAnnotations();
        
        services.AddHttpClient<IFundaOfferApiClient, FundaOfferApiClient>((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<FundaOfferApiSettings>>().Value;
                client.BaseAddress = new Uri(settings.Url);
            }
        ).AddResilienceHandler("FundaOfferApiResiliencePipeline", builder =>
        {
            // Refer to https://www.pollydocs.org/strategies/retry.html#defaults for retry defaults
            builder.AddRetry(new HttpRetryStrategyOptions
            {
                MaxRetryAttempts = 5,
                Delay = TimeSpan.FromSeconds(5),
                BackoffType = DelayBackoffType.Exponential,
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .HandleResult(response => response.StatusCode == HttpStatusCode.Unauthorized)
            });
        });
        
        services.AddScoped<IFundaOfferApiProvider, FundaOfferApiProvider>();
        return services;
    }
    
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAgentReportHandler, AgentReportHandler>();
        return services;
    }
}