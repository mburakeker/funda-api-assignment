using Funda.ApiAssignment.Domain.Models;

namespace Funda.ApiAssignment.Domain.Handlers;

public class AssignmentCaseHandler(IAgentReportHandler agentReportHandler) : IAssignmentCaseHandler
{
    public Task<AgentOfferAggregate[]?> GetTop10AgentsInAmsterdamForSale()
        => agentReportHandler.GetTop10AgentsByOfferCount(OfferType.ForSale, "/Amsterdam/");

    public Task<AgentOfferAggregate[]?> GetTop10AgentsInAmsterdamWithGardenForSale()
        => agentReportHandler.GetTop10AgentsByOfferCount(OfferType.ForSale, "/Amsterdam/Tuin/");
    
    public Task<AgentOfferAggregate[]?> GetTop10AgentsInBussumWithGardenForSale()
        => agentReportHandler.GetTop10AgentsByOfferCount(OfferType.ForSale, "/Bussum/Tuin/");
}