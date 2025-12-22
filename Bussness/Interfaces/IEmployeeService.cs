using Entities;
using Entities.DTOs;
namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        
            Task<List<EmployeeDto>> GetAllEmployeesAsync();
            Task<Employee?> GetEmployeeByIdAsync(int i);
            Task AddEmployeeAsync(CreateEmployeeDto d);
            Task InActiveEmployeeByIdAsync(int i);
            Task ActiveEmployeeByIdAsync(int i);
            Task UpdateEmployeeAsync(EmployeeDto d);

        
    }
}
