using Application.BugChase.GetLeaderboardQuery;
using MediatR;

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
    }
}