using Domain.Entities.CodeRoastEntities;
using MediatR;

namespace Application.CodeRoast.RoastCodeCommand;

public record CodeRoastCommand(string CodeSnippet) : IRequest<CodeRoastResponse>;