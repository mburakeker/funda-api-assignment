using Funda.ApiAssignment.Domain.Handlers;
using Funda.ApiAssignment.Domain.Providers;
using Funda.ApiAssignment.Infrastructure.Providers;
using Funda.ApiAssignment.Infrastructure.Setup;
using Microsoft.Extensions.Options;

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
        );
        services.AddScoped<IFundaOfferApiProvider, FundaOfferApiProvider>();
        return services;
    }
    
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAgentReportHandler, AgentReportHandler>();
        services.AddScoped<IAssignmentCaseHandler, AssignmentCaseHandler>();
        return services;
    }
}