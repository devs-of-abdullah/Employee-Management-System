
using Entities;
using Entities.DTOs;

namespace Data.Interfaces
{
    public interface IEmployeeRepository
    {
         Task<List<EmployeeDto>> GetAllEmployeesAsync();
         Task<Employee?> GetEmployeeByIdAsync(int i);
         Task AddEmployeeAsync(Employee e);
        Task InActiveEmployeeByIdAsync(int i);
         Task ActiveEmployeeByIdAsync(int i);
         Task UpdateEmployeeAsync(Employee e);

    }
}
