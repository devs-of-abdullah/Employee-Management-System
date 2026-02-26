using Business.Interfaces;
using Data.Interfaces;

namespace Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) { _repo = repo; }

        public async Task<List<DTO.Employee.ReadEmployeeDto>> GetAllAsync()
        {
            var employees = await _repo.GetAllAsync();

            return employees.Select(e => MapToDto(e)).ToList();
        }

        public async Task<DTO.Employee.ReadEmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repo.GetByIdAsync(id);

            if (employee == null) return null;
            return MapToDto(employee);
        }

        public async Task AddAsync(DTO.Employee.CreateEmployeeDto dto)
        {
            var employee = new Entities.EmployeeEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                Salary = dto.Salary,
            };

            await _repo.AddAsync(employee);

            if (dto.DepartmentIDs != null)
            {
                foreach (var deptId in dto.DepartmentIDs)
                {
                    employee.EmployeeDepartments.Add(new Entities.EmployeeDepartmentEntity
                    {
                        EmployeeId = employee.Id,
                        DepartmentId = deptId
                    });
                }
                await _repo.UpdateAsync(employee);
            }

            if (dto.RoleIDs != null)
            {
                foreach (var roleId in dto.RoleIDs)
                {
                    employee.EmployeeRoles.Add(new Entities.EmployeeRoleEntity
                    {
                        EmployeeId = employee.Id,
                        RoleId = roleId
                    });
                }
                await _repo.UpdateAsync(employee);
            }
        }

        public async Task UpdateAsync(int id, DTO.Employee.UpdateEmployeeDto dto)
        {
            var employee = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Employee not found");

            if (dto.FirstName != null) employee.FirstName = dto.FirstName;
            if (dto.LastName != null) employee.LastName = dto.LastName;
            if (dto.PhoneNumber != null) employee.PhoneNumber = dto.PhoneNumber;
            if (dto.Salary.HasValue) employee.Salary = dto.Salary.Value;

            await _repo.UpdateAsync(employee);
        }

        public async Task ActivateAsync(int id) => await _repo.SetActiveAsync(id, true);

        public async Task DeactivateAsync(int id) => await _repo.SetActiveAsync(id, false);

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        static DTO.Employee.ReadEmployeeDto MapToDto(Entities.EmployeeEntity e) =>
            new DTO.Employee.ReadEmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HireDate = e.HireDate,
                Salary = e.Salary,
                IsActive = e.IsActive,
                DepartmentIDs = e.EmployeeDepartments.Select(ed => ed.DepartmentId).ToArray(),
                RoleIDs = e.EmployeeRoles.Select(er => er.RoleId).ToArray()
            };
    }

}
