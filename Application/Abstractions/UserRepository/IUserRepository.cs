using Domain.Entities.Auth;

namespace Application.Abstractions.UserRepository;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string username);
    Task AddAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<bool> ExistsAsync(string username) =>
        Task.FromResult(_users.Any(u => u.Username == username));

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByUsernameAsync(string username) =>
        Task.FromResult(_users.FirstOrDefault(u => u.Username == username));
}