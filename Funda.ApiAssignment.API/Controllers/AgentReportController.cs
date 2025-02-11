using Funda.ApiAssignment.Domain.Handlers;
using Funda.ApiAssignment.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Funda.ApiAssignment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentReportController(IAgentReportHandler agentReportHandler) : ControllerBase
{
    [HttpGet("top-10-agents-in-amsterdam-for-sale")]
    public async Task<ActionResult<AgentOfferAggregate[]>> GetTop10AgentsInAmsterdamForSale()
    {
        try
        {
            var agents = await agentReportHandler.GetTop10AgentsByOfferCount(OfferType.ForSale, "/Amsterdam/");
            if (agents == null) return NotFound();
            return Ok(agents);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    [HttpGet("top-10-agents-in-amsterdam-with-garden-for-sale")]
    public async Task<ActionResult<AgentOfferAggregate[]>> GetTop10AgentsInAmsterdamWithGardenForSale()
    {
        try
        {
            var agents = await agentReportHandler.GetTop10AgentsByOfferCount(OfferType.ForSale, "/Amsterdam/Tuin/");
            if (agents == null) return NotFound();
            return Ok(agents);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
}