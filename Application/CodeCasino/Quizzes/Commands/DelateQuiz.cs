using Domain.Entities.Quizzes;
using MediatR;

namespace Application.Quizzes.Commands;

public record DeleteQuiz(QuizId QuizId) : IRequest;