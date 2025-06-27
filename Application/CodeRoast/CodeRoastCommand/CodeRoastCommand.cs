using Domain.Entities.CodeRoastEntities;
using Domain.Repository.CodeRoastRepositories;
using MediatR;

namespace Application.CodeRoast.RoastCodeCommand;

public record RoastCodeCommand(string CodeSnippet) : IRequest<CodeRoastResponse>;

public class RoastCodeHandler : IRequestHandler<RoastCodeCommand, CodeRoastResponse>
{
    private readonly ICodeRoastRepository _roastService;
    public RoastCodeHandler(ICodeRoastRepository roastService) => _roastService = roastService;

    public async Task<CodeRoastResponse> Handle(RoastCodeCommand request, CancellationToken cancellationToken)
    {
        return await _roastService.RoastCodeAsync(new CodeRoastRequest(request.CodeSnippet));
    }
}