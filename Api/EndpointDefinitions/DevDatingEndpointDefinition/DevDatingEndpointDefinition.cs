using Application.DevDating.CreateProfileCommands;
using Application.DevDating.GetMatchesQueries;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Api.EndpointDefinitions.DevDatingEndpointDefinition;

public static class DevDatingEndpointDefinition
{
    public static void MapDevDatingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/dating/profile", async ([FromBody] CreateProfileCommand command, ISender sender)
            => Results.Ok(await sender.Send(command)));

        app.MapGet("/api/dating/matches/{username}", async (string username, ISender sender)
            => Results.Ok(await sender.Send(new GetMatchesQuery(username))));

        app.MapPost("/api/dating/match", async ([FromBody] AutoMatchCommand command, ISender sender)
            => Results.Ok(await sender.Send(command)));

        app.MapPost("/api/dating/chat", async ([FromBody] SendMessageCommand command, ISender sender)
            => Results.Ok(await sender.Send(command)));
    }
}
