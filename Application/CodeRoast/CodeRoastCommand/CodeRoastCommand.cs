using Domain.Entities.CodeRoastEntities;
using Domain.Repository.CodeRoastRepositories;
using MediatR;

namespace Application.CodeRoast.RoastCodeCommand;

public record CodeRoastCommand(string CodeSnippet) : IRequest<CodeRoastResponse>
{
    
}

public class RoastCodeHandler : IRequestHandler<CodeRoastCommand, CodeRoastResponse>
{
    private readonly ICodeRoastRepository _roastService;
    public RoastCodeHandler(ICodeRoastRepository roastService) => _roastService = roastService;

    public async Task<CodeRoastResponse> Handle(CodeRoastCommand request, CancellationToken cancellationToken)
    {
        return await _roastService.RoastCodeAsync(new CodeRoastRequest(request.CodeSnippet));
    }
}