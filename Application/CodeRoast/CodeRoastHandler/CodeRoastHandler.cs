using Application.CodeRoast.RoastCodeCommand;
using Domain.Entities.CodeRoastEntities;
using Domain.Repository.CodeRoastRepositories;
using MediatR;

namespace Application.CodeRoast.CodeRoastHandler;

public class CodeRoastHandler : IRequestHandler<CodeRoastCommand, CodeRoastResponse>
{
    private readonly ICodeRoastRepository _roastRepository;
    public CodeRoastHandler(ICodeRoastRepository repository) => _roastRepository = repository;

    public async Task<CodeRoastResponse> Handle(CodeRoastCommand request, CancellationToken cancellationToken)
    {
        var domainRequest = new CodeRoastRequest(request.CodeSnippet); 
        return await _roastRepository.RoastCodeAsync(domainRequest);
    }
}