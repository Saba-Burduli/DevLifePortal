using Domain.Entities.Questions;
using MediatR;

namespace Application.Questions.Commands;

public record DeleteQuestion(QuestionId QuestionId) : IRequest;
