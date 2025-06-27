using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CodeRoastDbContext;

public class CodeRoastDbContext : DbContext
{
    public CodeRoastDbContext(DbContextOptions<CodeRoastDbContext> options) : base(options) {}
    public DbSet<RoastLog> RoastLogs => Set<RoastLog>();
}

public class RoastLog
{
    public int Id { get; set; }
    public string CodeSnippet { get; set; } = default!;
    public string Verdict { get; set; } = default!;
    public string RoastMessage { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}