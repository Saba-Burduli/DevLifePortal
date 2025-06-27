using Domain.Entities.ShowCodeEntities;
using Domain.Repository.ShowCodeRepositories;
using MediatR;

namespace Application.ShowCode.AnalyzeRepoQueries;

public record AnalyzeRepoQuery(string AccessToken, string RepoOwner, string RepoName) : IRequest<CodeProfile>;

public class AnalyzeRepoHandler : IRequestHandler<AnalyzeRepoQuery, CodeProfile>
{
    private readonly IShowCodeRepository _service;
    public AnalyzeRepoHandler(IShowCodeRepository service) => _service = service;

    public async Task<CodeProfile> Handle(AnalyzeRepoQuery request, CancellationToken cancellationToken)
        => await _service.AnalyzeRepositoryAsync(request.AccessToken, request.RepoOwner, request.RepoName);
}