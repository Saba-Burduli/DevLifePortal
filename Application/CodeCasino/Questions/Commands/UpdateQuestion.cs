using Domain.Entities.Questions;
using MediatR;

namespace Application.Questions.Commands;

public record UpdateQuestion(QuestionId Id, string Prompt, string Answer) : IRequest<Question>;
