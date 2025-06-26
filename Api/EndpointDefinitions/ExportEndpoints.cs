using Api.Abstractions;
using MediatR;
using Application.Abstractions.Export;
using Application.Quizzes.Queries;
using Domain.Entities.Quizzes;

namespace Api.EndpointDefinitions;


public class ExportEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var exports = app.MapGroup("/api/export");

        exports.MapGet("/getAvailableExporters/", GetAvailableExporters);
        
        exports.MapGet("/export/{quizId:guid}", ExportQuiz);
    }
    
    private IResult GetAvailableExporters(IExportServiceProvider exportServiceProvider)
    {
        var availableExporters = exportServiceProvider.GetAvailableExporters();
        
        return TypedResults.Ok(availableExporters);
    }
    
    private async Task<IResult> ExportQuiz(IMediator mediator, IExportServiceProvider exportServiceProvider, string format, string fileName, Guid quizId)
    {
        var getQuiz = new GetQuizById(new QuizId(quizId)); // maybe Quiz(quizId)
        var quiz = await mediator.Send(getQuiz);

        var exporter = exportServiceProvider.GetExportService(format);

        if (exporter is null)
        {
            return TypedResults.BadRequest("Unsupported export format.");
        }
        
        var exportedData = exporter.ExportQuiz(quiz);

        return TypedResults.File(exportedData, "application/octet-stream", $"{fileName}{exporter.Extension}");
    }
}