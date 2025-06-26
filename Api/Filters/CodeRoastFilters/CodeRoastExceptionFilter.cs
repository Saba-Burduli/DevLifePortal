using Domain.Exceptions.CodeRoastExceptions;

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
            return Results.Problem(ex.Message, statusCode: 500);
        }
    }
}