using Application.Quizzes.Commands;

namespace Api.Filters.Quizzes;

public class UpdateQuizValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var quiz = context.GetArgument<UpdateQuiz>(1);
        
        if (string.IsNullOrEmpty(quiz.Title)) return await Task.FromResult(Results.BadRequest("Invalid title."));
        
        return await next(context);
    }
}