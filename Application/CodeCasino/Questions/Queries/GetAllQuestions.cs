using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using MediatR;

namespace Application.Questions.Queries;

public record GetAllQuestions(
    string? SortColumn, 
    string? SortOrder, 
    int Page, 
    int PageSize, 
    string? Prompt,
    QuizId? QuizId) : IRequest<ICollection<Question>>;
