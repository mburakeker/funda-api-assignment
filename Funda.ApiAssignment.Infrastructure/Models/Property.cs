namespace Funda.ApiAssignment.Infrastructure.Models;

public class Property
{
    public Guid Id { get; init; }
    public int MakelaarId { get; init; }
    public required string MakelaarNaam { get; init; }
}