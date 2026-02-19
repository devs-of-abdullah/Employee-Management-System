

using Business.Interfaces;
using Data.Interfaces;
using DTO.Department;
using Entity;
namespace Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) { _repo = repo; }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repo.GetAllAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description

            }).ToList();
        }
        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _repo.GetByIdAsync(id);

            if (department == null) return null;

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

        }
        public async Task<int> CreateAsync(CreateDepartmentDto dto)
        {
            var entity = new DepartmentEntity
            {
                Name = dto.Name,
                Description = dto.Description,
            };
            return await _repo.CreateAsync(entity);
        }
        public async Task UpdateDepartmentDescriptionAsync(int Id,  UpdateDepartmentDescriptionDto dto)
        {
            var department = await _repo.GetByIdAsync(Id)
            ?? throw new KeyNotFoundException("Department not found");

            department.Description = dto.Description;

            await _repo.UpdateAsync(department);
        }
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        public async Task AddEmployeeAsync(int departmentId, int employeeId)
        {
            await _repo.AddEmployeeToDepartmentAsync(departmentId, employeeId);
        }
        public async Task RemoveEmployeeAsync(int departmentId, int employeeId)
        {
            await _repo.RemoveEmployeeFromDepartmentAsync(departmentId, employeeId);
        }
    }
}
