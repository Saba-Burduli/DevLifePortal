using Domain.Entities.Quizzes;
using MediatR;

namespace Application.Quizzes.Queries;

public record GetQuizById(QuizId QuizId) : IRequest<Quiz>;