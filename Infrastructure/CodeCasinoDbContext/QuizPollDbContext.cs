using Domain.Entities.Questions;
using Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class QuizPollDbContext : DbContext
{
    public QuizPollDbContext(DbContextOptions<QuizPollDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server= .\\\\SQLEXPRESS;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizPollDbContext).Assembly);
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
}