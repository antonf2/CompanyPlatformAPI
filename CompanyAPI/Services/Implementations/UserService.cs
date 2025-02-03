using CompanyAPI.Data;
using CompanyAPI.Data.Entities;
using CompanyAPI.DTOs;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class UserService : IUserService
{
    private readonly ContextDAL _context;

    public UserService(ContextDAL context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(u => new UserDto(u));
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : new UserDto(user);
    }

    public async Task<bool> CreateUserAsync(CreateUserDto userDto)
    {
        if (await UserExists(userDto.Username))
        {
            return false;
        }

        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = HashPassword(userDto.Password),
            Email = userDto.Email,
            Role = userDto.Role,
            IsActive = userDto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto userDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.Role = userDto.Role;
        user.IsActive = userDto.IsActive;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User> CreateUser(CreateUserDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            PasswordHash = HashPassword(userDto.Password),
            Email = userDto.Email,
            Role = userDto.Role,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            return null;
        return user;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = Encoding.UTF8.GetBytes(password);
            for (int i = 0; i < 1000; i++)
            {
                hashedBytes = sha256.ComputeHash(hashedBytes);
            }

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }
    }

        private bool VerifyPasswordHash(string password, string storedHash)
    {
        var hashedPassword = HashPassword(password);
        return hashedPassword == storedHash;
    }
}
