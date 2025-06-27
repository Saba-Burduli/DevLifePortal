using Domain.Entities.ShowCodeEntities;

namespace Domain.Repository.ShowCodeRepositories;

public interface IShowCodeRepository
{
    Task<CodeProfile> AnalyzeRepositoryAsync(string accessToken, string repoOwner, string repoName);

}