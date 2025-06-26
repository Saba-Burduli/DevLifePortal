using Domain.Entities.Quizzes;
using MediatR;

namespace Application.Quizzes.Queries;

public record GetAllQuizzes(
    string? SortColumn, 
    string? SortOrder, 
    int Page, 
    int PageSize, 
    string? Title) : IRequest<ICollection<Quiz>>;