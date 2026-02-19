
using Entity;

namespace Data.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<int> CreateAsync(DepartmentEntity department);
        Task DeleteAsync(int id);
        Task UpdateAsync(DepartmentEntity department);
        Task<DepartmentEntity?> GetByIdAsync(int id);
        Task<List<DepartmentEntity>> GetAllAsync();
        Task AddEmployeeToDepartmentAsync(int departmentId, int employeeID);
        Task RemoveEmployeeFromDepartmentAsync(int departmentId, int employeeID);
    }
}
