using CompanyAPI.Data.Entities;
using CompanyAPI.Data;
using CompanyAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ContextDAL _context;

        public UserService(ContextDAL context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User> GetUserByIdAsync(int id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
