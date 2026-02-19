using DTO.Role;
namespace Business.Interfaces
{
    public interface IRoleService
    {

        Task<int> CreateAsync(CreateRoleDto role);
        Task DeleteAsync(int id);
        Task<RoleDto?> GetByIdAsync(int id);
        Task<List<RoleDto>> GetAllAsync();
        Task AddEmployeeAsync(int roleId, int employeeID);
        Task RemoveEmployeeAsync(int roleId, int employeeID);

    }
}
