using Domain.Entities.DevDatingEntities;

namespace Domain.Repository.DevDatingRepositories;

public interface IDevDatingRepository
{
    Task<DevProfile> CreateProfileAsync(DevProfile profile);
    Task<List<DevProfile>> GetPotentialMatchesAsync(string username);
    Task<DevMatch> AutoMatchAsync(string username);
    Task<string> GetAIResponseAsync(string fromUser, string toUser, string message);
}