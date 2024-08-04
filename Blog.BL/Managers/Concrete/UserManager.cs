using Blog.BL.Managers.Abstract;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Concrete
{
    public class UserManager : ManagerBase<User>, IManager<User>
    {
        public UserManager(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            return await _context.Users
                .Include(u => u.Rolee)
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task AddAsync(User user)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            // Role kontrolü ve var olan Role ataması
            if (user.Rolee == null || string.IsNullOrWhiteSpace(user.Rolee.RoleName))
            {
                user.Rolee = await _context.Roles.FindAsync(user.RoleId) ?? throw new InvalidOperationException("Role does not exist.");
            }
            else
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == user.Rolee.RoleName);
                if (existingRole != null)
                {
                    user.Rolee = existingRole;
                }

            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

    }
}
