
using Entities;

namespace Data.Interfaces
{
    public interface IEmployeeRepository
    {
         Task<List<EmployeeEntity>> GetAllAsync();
         Task<EmployeeEntity?> GetByIdAsync(int id);
         Task AddAsync(EmployeeEntity employee);
         Task SetActiveAsync(int id, bool isActive);
         Task UpdateAsync(EmployeeEntity employee);
         Task DeleteAsync(int i);
    }
}
