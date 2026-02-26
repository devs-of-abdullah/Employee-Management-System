using Data.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) { _context = context; }

        public async Task<List<DepartmentEntity>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.EmployeeDepartments)
                    .ThenInclude(ed => ed.Employee)
                .ToListAsync();
        }

        public async Task<DepartmentEntity?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.EmployeeDepartments)
                    .ThenInclude(ed => ed.Employee)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<int> CreateAsync(DepartmentEntity department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }

        public async Task UpdateAsync(DepartmentEntity department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id)
                ?? throw new KeyNotFoundException("Department not found");
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        public async Task AddEmployeeToDepartmentAsync(int departmentId, int employeeId)
        {
            var exists = await _context.EmployeeDepartments
                .AnyAsync(ed => ed.DepartmentId == departmentId && ed.EmployeeId == employeeId);

            if (exists) return;

            var relation = new EmployeeDepartmentEntity
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId,
                AssignedDate = DateTime.UtcNow
            };

            await _context.EmployeeDepartments.AddAsync(relation);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEmployeeFromDepartmentAsync(int departmentId, int employeeId)
        {
            var relation = await _context.EmployeeDepartments
                .FirstOrDefaultAsync(ed => ed.DepartmentId == departmentId && ed.EmployeeId == employeeId);

            if (relation == null) return;

            _context.EmployeeDepartments.Remove(relation);
            await _context.SaveChangesAsync();
        }
    }
}
