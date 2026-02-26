
using Entities;

namespace Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeEntity>> GetAllAsync();
        Task<EmployeeEntity?> GetByIdAsync(int id);
        Task AddAsync(EmployeeEntity employee);
        Task UpdateAsync(EmployeeEntity employee);
        Task SetActiveAsync(int id, bool isActive);
        Task DeleteAsync(int id);
    }
}
