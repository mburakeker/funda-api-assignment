using System.ComponentModel.DataAnnotations;

namespace Funda.ApiAssignment.Infrastructure.Setup;

public class FundaOfferApiSettings
{
    public const string SettingName = "FundaOfferApi";
    [Required]
    [Url]
    public required string Url { get; set; }
    [Required]
    public required string ApiKey { get; set; }
}