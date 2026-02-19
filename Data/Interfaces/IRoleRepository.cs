

using Entities;

namespace Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<int> CreateAsync(RoleEntity role);
        Task DeleteAsync(int id);
        Task UpdateAsync(RoleEntity role);
        Task<RoleEntity?> GetByIdAsync(int id);
        Task<List<RoleEntity>> GetAllAsync();
        Task AddEmployeeToRoleAsync(int roleId, int employeeID);
        Task RemoveEmployeeFromRoleAsync(int roleId, int employeeID);
    }
}