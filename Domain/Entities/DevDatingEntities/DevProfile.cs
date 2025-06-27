namespace Domain.Entities.DevDatingEntities;

public class DevProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Preferences { get; set; } = default!;
    public string Bio { get; set; } = default!;
    public List<string> TechStack { get; set; } = new();
    public bool IsMatched { get; set; }
    
    public string Personality { get; set; } = "friendly"; // new

}