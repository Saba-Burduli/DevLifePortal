namespace Domain.Entities.BugChaseEntities;

public class BugChasePlayer
{
    public string ConnectionId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public int Score { get; set; }
    public string CharacterSkin { get; set; } = "Default";
    public List<string> Achievements { get; set; } = new();
}

/*
 *     public async Task UpdateScoreAsync(string username, int score)
   {
       return await _context.Players
           .OrderByDescending(p => p.Score)
           .Take(10)
           .Select(p => new LeaderboardEntry { UserName = p.Username, Score = p.Score })
           .ToListAsync();    }

   public async Task<List<LeaderboardEntry>> GetTopScoresAsync()
   {
       throw new NotImplementedException();
   }

   public async Task UnlockAchievementAsync(string username, string achievement)
   {
       var player = await _context.Players.FirstOrDefaultAsync(p => p.User == username);
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
 */