using Domain.Entities.Quizzes;

namespace Application.Abstractions.Export;

public interface IExportService
{
    string Format { get; }
    string Extension { get; }
    byte[] ExportQuiz(Quiz quiz);
}