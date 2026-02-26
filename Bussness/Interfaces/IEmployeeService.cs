
using DTO.Employee;
namespace Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<ReadEmployeeDto>> GetAllAsync();
        Task<ReadEmployeeDto?> GetByIdAsync(int id);
        Task AddAsync(CreateEmployeeDto dto);
        Task UpdateAsync(int id, UpdateEmployeeDto dto);
        Task ActivateAsync(int id);
        Task DeactivateAsync(int id);
        Task DeleteAsync(int id);
    }
}
