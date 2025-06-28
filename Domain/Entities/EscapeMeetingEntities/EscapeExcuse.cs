namespace Domain.Entities.EscapeMeetingEntities;

public class EscapeExcuse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Category { get; set; } = default!; // e.g. Daily Stand, Client Meeting
    public string Type { get; set; } = default!;     // e.g. Technical, Personal
    public string Text { get; set; } = default!;
    public int ConvincibilityScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}