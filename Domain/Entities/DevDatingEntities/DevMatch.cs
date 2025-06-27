namespace Domain.Entities.DevDatingEntities;

public class DevMatch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserA { get; set; } = default!;
    public string UserB { get; set; } = default!;
    
    public DateTime MatchedAt { get; set; } = DateTime.UtcNow;
}