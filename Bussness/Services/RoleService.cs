

using Business.Interfaces;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        readonly Data.Interfaces.IRoleRepository _repo;
        public RoleService(Data.Interfaces.IRoleRepository repo) { _repo = repo; }

        public async Task<List<DTO.Role.RoleDto>> GetAllAsync()
        {
            var roles = await _repo.GetAllAsync();
            return roles.Select(r => MapToDto(r)).ToList();
        }

        public async Task<DTO.Role.RoleDto?> GetByIdAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null) return null;
            return MapToDto(role);
        }

        public async Task<int> CreateAsync(DTO.Role.CreateRoleDto dto)
        {
            var entity = new Entities.RoleEntity
            {
                Name = dto.Name,
                Description = dto.Description
            };
            return await _repo.CreateAsync(entity);
        }

        public async Task UpdateNameAsync(int id, DTO.Role.UpdateRoleNameDto dto)
        {
            var role = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Role not found");

            role.Name = dto.Name;
            await _repo.UpdateAsync(role);
        }

        public async Task UpdateDescriptionAsync(int id, DTO.Role.UpdateRoleDescriptionDto dto)
        {
            var role = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Role not found");

            role.Description = dto.Description;
            await _repo.UpdateAsync(role);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        public async Task AddEmployeeAsync(int roleId, int employeeId)
            => await _repo.AddEmployeeToRoleAsync(roleId, employeeId);

        public async Task RemoveEmployeeAsync(int roleId, int employeeId)
            => await _repo.RemoveEmployeeFromRoleAsync(roleId, employeeId);

        static DTO.Role.RoleDto MapToDto(Entities.RoleEntity r) =>
            new DTO.Role.RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Employees = r.EmployeeRoles.Select(er => new DTO.Employee.ReadEmployeeDto
                {
                    Id = er.Employee.Id,
                    FirstName = er.Employee.FirstName,
                    LastName = er.Employee.LastName,
                    Email = er.Employee.Email,
                    PhoneNumber = er.Employee.PhoneNumber,
                    HireDate = er.Employee.HireDate,
                    Salary = er.Employee.Salary,
                    IsActive = er.Employee.IsActive
                }).ToList()
            };
    }
}
