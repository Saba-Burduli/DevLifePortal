namespace Domain.Entities.BugChaseEntities;

public class BugChasePlayer
{
    public string ConnectionId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public int Score { get; set; }
    public string CharacterSkin { get; set; } = "Default";
    public List<string> Achievements { get; set; } = new();
}

