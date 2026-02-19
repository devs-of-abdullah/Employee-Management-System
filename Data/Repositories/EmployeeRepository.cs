using Data.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<List<EmployeeEntity>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
           
           
        }
        public async Task<EmployeeEntity?> GetByIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id ==  id);
        }
        public async Task AddAsync(EmployeeEntity employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
           
        }
        public async Task UpdateAsync(EmployeeEntity employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        public async Task SetActiveAsync(int id, bool isActive)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return;
           

            employee.IsActive = isActive;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null) return;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

    }
}
