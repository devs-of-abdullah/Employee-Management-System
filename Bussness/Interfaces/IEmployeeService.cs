
using DTO.Employee;
namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        
            Task<List<ReadEmployeeDto>> GetAllAsync();
            Task<ReadEmployeeDto> GetByIdAsync(int id);
            Task AddAsync(CreateEmployeeDto employee);
            Task DeactivateAsync(int id);
            Task ActivateAsync(int id);
            Task DeleteAsync(int id);
        
    }
}
