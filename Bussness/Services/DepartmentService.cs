

using Business.Interfaces;
using Data.Interfaces;
namespace Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) { _repo = repo; }

        public async Task<List<DTO.Department.DepartmentDto>> GetAllAsync()
        {
            var departments = await _repo.GetAllAsync();
            return departments.Select(d => MapToDto(d)).ToList();
        }

        public async Task<DTO.Department.DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _repo.GetByIdAsync(id);
            if (department == null) return null;
            return MapToDto(department);
        }

        public async Task<int> CreateAsync(DTO.Department.CreateDepartmentDto dto)
        {
            var entity = new Entities.DepartmentEntity
            {
                Name = dto.Name,
                Description = dto.Description
            };
            return await _repo.CreateAsync(entity);
        }

        public async Task UpdateNameAsync(int id, DTO.Department.UpdateDepartmentNameDto dto)
        {
            var department = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Department not found");

            department.Name = dto.Name;
            await _repo.UpdateAsync(department);
        }

        public async Task UpdateDescriptionAsync(int id, DTO.Department.UpdateDepartmentDescriptionDto dto)
        {
            var department = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Department not found");

            department.Description = dto.Description;
            await _repo.UpdateAsync(department);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task AddEmployeeAsync(int departmentId, int employeeId)
            => await _repo.AddEmployeeToDepartmentAsync(departmentId, employeeId);

        public async Task RemoveEmployeeAsync(int departmentId, int employeeId)
            => await _repo.RemoveEmployeeFromDepartmentAsync(departmentId, employeeId);

        static DTO.Department.DepartmentDto MapToDto(Entities.DepartmentEntity d) =>
            new DTO.Department.DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Employees = d.EmployeeDepartments.Select(ed => new DTO.Employee.ReadEmployeeDto
                {
                    Id = ed.Employee.Id,
                    FirstName = ed.Employee.FirstName,
                    LastName = ed.Employee.LastName,
                    Email = ed.Employee.Email,
                    PhoneNumber = ed.Employee.PhoneNumber,
                    HireDate = ed.Employee.HireDate,
                    Salary = ed.Employee.Salary,
                    IsActive = ed.Employee.IsActive
                }).ToList()
            };
    }
}
