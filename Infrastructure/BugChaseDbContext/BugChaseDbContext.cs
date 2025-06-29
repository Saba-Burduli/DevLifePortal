using Domain.Entities.BugChaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BugChaseDbContext;
public class BugChaseDbContext : DbContext
{
    public BugChaseDbContext(DbContextOptions<BugChaseDbContext> options) : base(options) {}
    
    public DbSet<BugChasePlayer> Players => Set<BugChasePlayer>();
    public DbSet<LeaderboardEntry> LeaderBoards { get; set; }
}