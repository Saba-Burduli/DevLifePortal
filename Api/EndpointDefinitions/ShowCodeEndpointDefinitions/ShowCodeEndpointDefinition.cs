using Application.ShowCode.AnalyzeRepoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.ShowCodeEndpointDefinitions;

public static class ShowCodeEndpointDefinition
{
    public static void MapShowCodeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/showcode/analyzeGithub", async ([FromBody] AnalyzeRepoQuery query, ISender sender)
            => Results.Ok(await sender.Send(query)));
    }
}
