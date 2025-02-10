using Funda.ApiAssignment.API.Setup;
using Funda.ApiAssignment.Domain.Handlers;

namespace Funda.ApiAssignment.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddDomainServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }
        
        app.MapGet("/top-10-agents-in-amsterdam-for-sale", (IAssignmentCaseHandler assignmentCaseHandler) 
            => assignmentCaseHandler.GetTop10AgentsInAmsterdamForSale());
        
        app.MapGet("/top-10-agents-in-amsterdam-with-garden-for-sale", (IAssignmentCaseHandler assignmentCaseHandler) 
            => assignmentCaseHandler.GetTop10AgentsInAmsterdamWithGardenForSale());
        
        app.MapGet("/top-10-agents-in-bussum-with-garden-for-sale", (IAssignmentCaseHandler assignmentCaseHandler) 
            => assignmentCaseHandler.GetTop10AgentsInBussumWithGardenForSale());

        app.Run();
    }
}