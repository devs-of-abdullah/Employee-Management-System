using DTO.Department;

namespace Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateDepartmentDto dto);
        Task UpdateDescriptionAsync(int id, UpdateDepartmentDescriptionDto dto);
        Task UpdateNameAsync(int id, UpdateDepartmentNameDto dto);
        Task DeleteAsync(int id);
        Task AddEmployeeAsync(int departmentId, int employeeId);
        Task RemoveEmployeeAsync(int departmentId, int employeeId);
    }
}
