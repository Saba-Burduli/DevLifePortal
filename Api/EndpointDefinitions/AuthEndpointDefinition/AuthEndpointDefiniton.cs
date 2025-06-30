using Application.DTOs;
using Domain.Repository.UserRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndpointDefinitions.AuthEndpointDefinition;

public static class AuthEndpointDefinition
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/register", async (UserDto userDto, IUserService service) =>
        {
            if (await service.ExistsAsync(userDto.Username))
                return Results.BadRequest("Username already taken");

            await service.RegisterAsync(userDto);
            return Results.Ok("Registration successful. Redirect to home.");
        });

        app.MapPost("/auth/login", async (HttpContext context, [FromBody] string username, IUserService service) =>
        {
            var user = await service.GetByUsernameAsync(username);
            if (user is null) return Results.BadRequest("User not found");

            context.Session.SetString("Username", user.Username);
            return Results.Ok($"Welcome, {user.FirstName}!");
        });

        app.MapPost("/auth/logout", (HttpContext context) =>
        {
            context.Session.Clear();
            return Results.Ok("Logged out successfully.");
        });

        app.MapGet("/auth/me", (HttpContext context) =>
        {
            var username = context.Session.GetString("Username");
            return username is null
                ? Results.Unauthorized()
                : Results.Ok(new { Username = username });
        });
    }
}