using Blog.BL.Managers.Abstract;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Concrete
{
    public class UserManager : ManagerBase<User>, IManager<User>
    {
        private readonly AppDbContext _context;

        public UserManager(AppDbContext context) : base(context)
        {
            _context = context;
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
                    user.Role = existingRole.RoleName; // Role adını senkronize et
                }
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                // Kullanıcı adı benzersizliğini kontrol et
                if (await _context.Users.AnyAsync(u => u.UserName == user.UserName && u.Id != user.Id))
                {
                    throw new InvalidOperationException("Username already exists.");
                }

                existingUser.UserName = user.UserName;
                existingUser.Name = user.Name;
                existingUser.LastName = user.LastName;
                existingUser.Mail = user.Mail;
                existingUser.RoleId = user.RoleId;
                existingUser.ProfilePhotoUrl = user.ProfilePhotoUrl;

                // Role adını RoleId'ye göre güncelle
                var roleEntity = await _context.Roles.FindAsync(user.RoleId);
                if (roleEntity != null)
                {
                    existingUser.Rolee = roleEntity;
                    existingUser.Role = roleEntity.RoleName;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
