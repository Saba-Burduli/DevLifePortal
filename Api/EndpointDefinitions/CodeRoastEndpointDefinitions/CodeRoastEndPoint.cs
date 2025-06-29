using Application.CodeRoast.RoastCodeCommand;
using Domain.Hubs.CodeRoastHub;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.CodeRoastEndpointDefinitions;

public static class CodeRoastEndPoint
{
    public static void MapCodeRoastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/roast", async (
                [FromBody] RoastCodeHandler command,
                ISender sender) =>
            {
                var response = await sender.Send(command);
                return Results.Ok(response);
            })
            .AddEndpointFilter<CodeRoastExceptionFilter>()
            .WithName("RoastCode");

        app.MapHub<CodeRoastHub>("/ws/coderoast");
    }
}