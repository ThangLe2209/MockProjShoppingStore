using Microsoft.EntityFrameworkCore;
using ShoppingStore.API.DbContexts;
using ShoppingStore.Model;
using ShoppingStore.Model.Entities;

namespace ShoppingStore.API.Services
{
	public class RoleRepository : IRoleRepository
	{
		private readonly IdentityDbContext _context;
		public RoleRepository(IdentityDbContext context)
		{
			_context = context ?? throw new ArgumentException(nameof(context));
		}

        public void AddRole(UserRole role)
        {
            _context.UserRoles.Add(role);
        }

        public void DeleteRole(UserRole role)
        {
            _context.UserRoles.Remove(role);
        }

        public async Task<UserRole> GetRoleById(Guid roleId)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<UserRole> GetRoleByName(string? value)
        {
            return await _context.UserRoles.FirstOrDefaultAsync(r => r.Value == value);
        }

        public async Task<IEnumerable<UserRole>> GetRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
