using Application.EscapeMeeting.GenerateExcuseQueries;
using Domain.Entities.EscapeMeetingEntities;
using Domain.Repository.EscapeMeetingRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.EscapeMeetingEndPointDefinitions;

public static class EscapeMeetingEndPointDefinition
{
    public static void MapEscapeMeetingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/escape/generate", async ([FromBody] GenerateExcuseQuery query, ISender sender)
            => Results.Ok(await sender.Send(query)));

        app.MapPost("/api/escape/favorite", async ([FromBody] EscapeExcuse excuse, IEscapeMeetingRepository service) =>
        {
            await service.SaveFavoriteAsync(excuse);
            return Results.Ok();
        });

        app.MapGet("/api/escape/favorites", async (IEscapeMeetingRepository service)
            => Results.Ok(await service.GetFavoritesAsync()));
    }
}