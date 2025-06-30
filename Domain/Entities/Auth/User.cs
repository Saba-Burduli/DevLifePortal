namespace Domain.Entities.Auth;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public string TechStack { get; set; } = default!;
    public string ExperienceLevel { get; set; } = default!;
    public string ZodiacSign { get; set; } = default!;
}