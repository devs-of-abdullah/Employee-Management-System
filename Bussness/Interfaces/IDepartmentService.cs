using DTO.Department;

namespace Business.Interfaces
{
    public interface IDepartmentService
    {
        
            Task<int> CreateAsync(CreateDepartmentDto department);
            Task DeleteAsync(int id);
      
            Task<DepartmentDto?> GetByIdAsync(int id);
            Task<List<DepartmentDto>> GetAllAsync();
            Task AddEmployeeAsync(int departmentId, int employeeID);
            Task RemoveEmployeeAsync(int departmentId, int employeeID);
        
    }
}
