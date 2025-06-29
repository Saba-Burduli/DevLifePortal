using Domain.Entities.BugChaseEntities;
using Domain.Repository.BugChaseRepositories;
using MongoDB.Driver.Linq;

namespace Infrastructure.Persistence.Repositories.BugChaseRepositories;

public class BugChaseRepository : IBugChaseRepository
{
    private readonly BugChaseDbContext.BugChaseDbContext _context;

    public BugChaseRepository(BugChaseDbContext.BugChaseDbContext context) => _context = context;

    public async Task UpdateScoreAsync(string username, int score)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
        if (player == null) return;
        player.Score = Math.Max(player.Score, score);
        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaderboardEntry>> GetTopScoresAsync()
    {
        return await _context.Players
            .OrderByDescending(p => p.Score)
            .Take(10)
            .Select(p => new LeaderboardEntry { Username = p.Username, Score = p.Score })
            .ToListAsync();
    }

    public async Task UnlockAchievementAsync(string username, string achievement)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
        if (player == null) return;
        if (!player.Achievements.Contains(achievement))
        {
            player.Achievements.Add(achievement);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ChangeCharacterSkinAsync(string username, string newSkin)
    {
        var player = await _context.Players.FirstOrDefaultAsync(p => p.Username == username);
        if (player == null) return;
        player.CharacterSkin = newSkin;
        await _context.SaveChangesAsync();
    }
}