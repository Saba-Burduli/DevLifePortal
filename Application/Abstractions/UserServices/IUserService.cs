using Application.DTOs;
using Application.Helpers;
using Domain.Entities.Auth;

namespace Domain.Repository.UserRepositories;

public interface IUserService
{
        Task<User> AddAsync(User user);
        Task<bool> ExistsAsync(string username);
        Task RegisterAsync(UserDto userDto);
        Task<User?> GetByUsernameAsync(string username);
}


public class UserService : IUserService
{
        private readonly IUserService _service;

        public UserService(IUserService service)
        {
                _service = service;
        }

        public async Task<User> AddAsync(User user)
        {
              return await _service.AddAsync(user);
        }

        public async Task<bool> ExistsAsync(string username) =>
                await _service.ExistsAsync(username);

        public async Task RegisterAsync(UserDto dto)
        {
                var user = new User
                {
                        Username = dto.Username,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        DateOfBirth = dto.DateOfBirth,
                        TechStack = dto.TechStack,
                        ExperienceLevel = dto.ExperienceLevel,
                        ZodiacSign = ZodiacHelper.GetZodiacSign(dto.DateOfBirth)
                };

                await _service.AddAsync(user);
        }

        public async Task<User?> GetByUsernameAsync(string username) =>
                await _service.GetByUsernameAsync(username);
}
