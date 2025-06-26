using Api.Abstractions;
using Api.Filters.Questions;
using Application.Questions.Commands;
using Application.Questions.Queries;
using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using MediatR;

namespace Api.EndpointDefinitions;

public class QuestionEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var questions = app.MapGroup("/api/questions");

        questions.MapGet("/", GetAllQuestions)
            .RequireRateLimiting("fixed");
        
        questions.MapPost("/", CreateQuestion)
            .AddEndpointFilter<CreateQuestionValidationFilter>();
        
        questions.MapPut("/", UpdateQuestion)
            .AddEndpointFilter<UpdateQuestionValidationFilter>();

        questions.MapDelete("/{questionId:guid}", DeleteQuestion);
    }
    
    private async Task<IResult> GetAllQuestions(
        IMediator mediator, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        string? prompt,
        Guid? quizId)
    {
        QuizId? id = null;
        
        if (quizId is not null)
        {
            id = new QuizId(quizId.Value);
        }

        var getAllQuestions = new GetAllQuestions(sortColumn, sortOrder, page, pageSize, prompt, id);
        var questions = await mediator.Send(getAllQuestions);

        return TypedResults.Ok(questions);
    }

    private async Task<IResult> CreateQuestion(IMediator mediator, CreateQuestion command)
    {
        var createdQuestion = await mediator.Send(command);

        return TypedResults.Ok(createdQuestion);
    }
    
    private async Task<IResult> UpdateQuestion(IMediator mediator, UpdateQuestion command)
    {
        var updatedQuestion = await mediator.Send(command);

        return TypedResults.Ok(updatedQuestion);
    }
    
    private async Task<IResult> DeleteQuestion(IMediator mediator, Guid questionId)
    {
        var deleteQuestion = new DeleteQuestion(new QuestionId(questionId));
        
        await mediator.Send(deleteQuestion);

        return TypedResults.NoContent();
    }
}