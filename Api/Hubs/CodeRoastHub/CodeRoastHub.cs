using Api.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace Domain.Hubs.CodeRoastHub;

public class CodeRoastHub : Hub { }

// CodeRoast/CodeRoastExceptionFilter.cs
public class CodeRoastExceptionFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (CodeRoastException ex)
        {
            return Results.Problem(title: "Code Roast Error", detail: ex.Message);
        }
    }
}