using Application.CodeRoast.RoastCodeCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.CodeRoastEndpointDefinitions;

public static class CodeRoastEndPoint
{
    public static void MapCodeRoastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/roast", async (
                [FromBody] CodeRoastCommand command,
                ISender sender) =>
            {
                var response = await sender.Send(command);
                return Results.Ok(response);
            })
            .AddEndpointFilter<CodeRoastExceptionFilter>()
            .WithName("RoastCode");
    }
}