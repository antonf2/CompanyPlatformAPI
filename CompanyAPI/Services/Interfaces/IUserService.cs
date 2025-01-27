using CompanyAPI.Data.Entities;
using CompanyAPI.DTOs;

namespace CompanyAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(CreateUserDto userDto);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UserExists(string username);
        Task<User> Authenticate(string username, string password);
        Task<User> CreateUser(CreateUserDto userDto);
    }
}