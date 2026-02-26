using Data.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) { _context = context; }

        public async Task<List<RoleEntity>> GetAllAsync()
        {
            return await _context.Roles
                .Include(r => r.EmployeeRoles)
                    .ThenInclude(er => er.Employee)
                .ToListAsync();
        }

        public async Task<RoleEntity?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Include(r => r.EmployeeRoles)
                    .ThenInclude(er => er.Employee)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> CreateAsync(RoleEntity role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role.Id;
        }

        public async Task UpdateAsync(RoleEntity role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id)
                ?? throw new KeyNotFoundException("Role not found");
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeToRoleAsync(int roleId, int employeeId)
        {
            var exists = await _context.EmployeeRoles
                .AnyAsync(er => er.RoleId == roleId && er.EmployeeId == employeeId);

            if (exists) return;

            var relation = new EmployeeRoleEntity
            {
                EmployeeId = employeeId,
                RoleId = roleId,
                AssignedDate = DateTime.UtcNow
            };

            await _context.EmployeeRoles.AddAsync(relation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEmployeeFromRoleAsync(int roleId, int employeeId)
        {
            var relation = await _context.EmployeeRoles
                .FirstOrDefaultAsync(er => er.RoleId == roleId && er.EmployeeId == employeeId);

            if (relation == null) return;

            _context.EmployeeRoles.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }
}

