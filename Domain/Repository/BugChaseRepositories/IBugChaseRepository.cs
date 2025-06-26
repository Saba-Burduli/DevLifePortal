using Domain.Entities.BugChaseEntities;

namespace Domain.Repository.BugChaseRepositories;

public interface IBugChaseRepository
{
    Task UpdateScoreAsync(string username, int score);
    Task<List<LeaderboardEntry>> GetTopScoresAsync();
    Task UnlockAchievementAsync(string username, string achievement);
    Task ChangeCharacterSkinAsync(string username, string newSkin);
}