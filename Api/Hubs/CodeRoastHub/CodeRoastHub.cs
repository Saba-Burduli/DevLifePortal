using Application.CodeRoast.RoastCodeCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs.CodeRoastHub;

public class CodeRoastHub : Hub
{   
    public static class CodeRoastEndPointDefinition
    {
        public static void MapCodeRoastEndpoints(/*this*/ IEndpointRouteBuilder app)
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
}

// CodeRoastEndPointDefinition.cs

/*public static class CodeRoastEndPointDefinition
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
}*/
