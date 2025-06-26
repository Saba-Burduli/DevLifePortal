using Application.Questions.Commands;

namespace Api.Filters.Questions;

 public class UpdateQuestionValidationFilter : IEndpointFilter
 {
     public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
     {
         var question = context.GetArgument<UpdateQuestion>(1);
        
         if (string.IsNullOrEmpty(question.Prompt)) return await Task.FromResult(Results.BadRequest("Invalid prompt."));
         if (string.IsNullOrEmpty(question.Answer)) return await Task.FromResult(Results.BadRequest("Invalid answer."));
        
         return await next(context);
     }
 }