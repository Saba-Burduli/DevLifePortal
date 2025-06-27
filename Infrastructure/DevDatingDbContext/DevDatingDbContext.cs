using Domain.Entities.DevDatingEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DevDatingDbContext;

public class DevDatingDbContext : DbContext
{
    public DevDatingDbContext(DbContextOptions<DevDatingDbContext> options) : base(options) {}
    public DbSet<DevProfile> Profiles => Set<DevProfile>();
    public DbSet<DevMatch> Matches => Set<DevMatch>();
}
