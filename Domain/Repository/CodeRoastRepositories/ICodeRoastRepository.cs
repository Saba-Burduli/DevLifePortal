using Domain.Entities.CodeRoastEntities;

namespace Domain.Repository.CodeRoastRepositories;

public interface ICodeRoastRepository
{ 
    Task<CodeRoastResponse> RoastCodeAsync(CodeRoastRequest request);
}