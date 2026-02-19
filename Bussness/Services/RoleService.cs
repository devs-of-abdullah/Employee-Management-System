

using Business.Interfaces;
using Data.Interfaces;
using Entities;
using DTO.Role;
namespace Business.Services
{
    public class RoleService : IRoleService
    {
        readonly IRoleRepository _repo;
        public RoleService(IRoleRepository repo) { _repo = repo; }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await _repo.GetAllAsync();

            return roles.Select(d => new RoleDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description

            }).ToList();
        }
        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _repo.GetByIdAsync(id);

            if (role == null) return null;

            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

        }
        public async Task<int> CreateAsync(CreateRoleDto dto)
        {
            var entity = new RoleEntity
            {
                Name = dto.Name,
                Description = dto.Description,
            };
            return await _repo.CreateAsync(entity);
        }
    
        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
        public async Task AddEmployeeAsync(int roleId, int employeeId)
        {
            await _repo.AddEmployeeToRoleAsync(roleId, employeeId);
        }
        public async Task RemoveEmployeeAsync(int roleId, int employeeId)
        {
            await _repo.RemoveEmployeeFromRoleAsync(roleId, employeeId);
        }
    }
}
