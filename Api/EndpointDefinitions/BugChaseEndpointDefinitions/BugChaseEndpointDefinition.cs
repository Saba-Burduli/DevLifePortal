using Api.Hubs.BugChaseHub;
using Application.BugChase.BugChaseCommands;
using Application.BugChase.ChangeSkinCommands;
using Application.BugChase.GetLeaderboardQuery;
using Application.BugChase.UnlockAchievementCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.BugChaseEndpointDefinitions;

public static class BugChaseEndpointDefinition
{
        public static void MapBugChaseEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/leaderboard", async (ISender sender) =>
            {
                var result = await sender.Send(new GetLeaderboardQuery());
                return Results.Ok(result);
            }).WithName("GetLeaderboard");

            app.MapPost("/api/score", async ([FromBody] UpdateScoreCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.Ok();
            }).WithName("UpdateScore");

            app.MapPost("/api/achievement", async ([FromBody] UnlockAchievementCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.Ok();
            }).WithName("UnlockAchievement");

            app.MapPost("/api/character/skin", async ([FromBody] ChangeSkinCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.Ok();
            }).WithName("ChangeSkin");

            app.MapHub<BugChaseHub>("/bugchase-hub");
        }
}
    