using Domain.Entities.Questions;
using MediatR;

namespace Application.Questions.Commands;

public record CreateQuestion(string Prompt, string Answer) : IRequest<Question>;
