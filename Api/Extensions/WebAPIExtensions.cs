using System.Threading.RateLimiting;
using Api.Abstractions;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Extensions;

public static class WebApiExtensions
{
    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitions = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                        && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.RegisterEndpoints(app);
        }
    }
    
    public static void AddRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
            rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
            {
                options.Window = TimeSpan.FromSeconds(1);
                options.PermitLimit = 10;
                options.QueueLimit = 0;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });
    }
}
