namespace Application.DTOs;

    public record UserDto(
        string Username,
        string FirstName,
        string LastName,
        DateOnly DateOfBirth,
        string TechStack,
        string ExperienceLevel);
