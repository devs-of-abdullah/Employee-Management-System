using Data.Interfaces;
using Entities;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) { _context = context; }
        public async Task<int> CreateAsync(DepartmentEntity department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department.Id;
        }
        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id) 
                ?? throw new KeyNotFoundException("department not found");

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
        public async Task<DepartmentEntity?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<DepartmentEntity>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }
        public async Task UpdateAsync(DepartmentEntity department)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
        }
        public async Task CreateDepartmentAsync(DepartmentEntity entity)
        {
            _context.Departments.Add(entity);
            await _context.SaveChangesAsync();

        }
        public async Task AddEmployeeToDepartmentAsync(int employeeId, int departmentId)
        {

            var relation = new EmployeeDepartmentEntity
            {
                EmployeeId = employeeId,
                DepartmentId = departmentId,
                AssignedDate = DateTime.UtcNow

            };
            await _context.Set<EmployeeDepartmentEntity>().AddAsync(relation);
            await _context.SaveChangesAsync();
            

        }
        public async Task RemoveEmployeeFromDepartmentAsync(int departmentId, int employeeId)
        {
            var relation = await _context.Set<EmployeeDepartmentEntity>().FindAsync(employeeId, departmentId);
           
            if (relation == null) 
                return; 

            _context.Set<EmployeeDepartmentEntity>().Remove(relation);
            await _context.SaveChangesAsync();
        }

    }
}
