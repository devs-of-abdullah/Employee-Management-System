using Data.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) { _context = context; }
        public async Task<int> CreateAsync( RoleEntity role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id)
                ?? throw new KeyNotFoundException("role not found");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
        public async Task<RoleEntity?> GetByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<RoleEntity>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task UpdateAsync(RoleEntity role)
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddEmployeeToRoleAsync(int employeeId,int roleId)
        {
            var employee = await _context.Employees.Include(e => e.EmployeeDepartments).FirstOrDefaultAsync(e => e.Id == employeeId);


            if (employee == null)
                throw new Exception("Employee not found.");

            employee.EmployeeRoles.Add(new EmployeeRoleEntity
            {
                EmployeeId = employeeId,
                RoleId = roleId
            });

            await _context.SaveChangesAsync();

           

            await _context.SaveChangesAsync();

        }
        public async Task RemoveEmployeeFromRoleAsync(int roleId, int employeeId)
        {
            var relation = await _context.Set<EmployeeRoleEntity>().FindAsync(employeeId, roleId);
            
            if (relation == null)
                return;

            _context.Set<EmployeeRoleEntity>().Remove(relation);

            await _context.SaveChangesAsync();
        }

    }
}

