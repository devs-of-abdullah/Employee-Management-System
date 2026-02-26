using DTO.Role;
namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateRoleDto dto);
        Task UpdateNameAsync(int id, UpdateRoleNameDto dto);
        Task UpdateDescriptionAsync(int id, UpdateRoleDescriptionDto dto);
        Task DeleteAsync(int id);
        Task AddEmployeeAsync(int roleId, int employeeId);
        Task RemoveEmployeeAsync(int roleId, int employeeId);
    }
}
