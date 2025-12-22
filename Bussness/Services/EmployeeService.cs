using Business.Interfaces;
using Data.Interfaces;
using Entities;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) 
        {
            _repo = repo;
        }
        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _repo.GetAllEmployeesAsync();
        }
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _repo.GetEmployeeByIdAsync(id);
        }
        public  async Task AddEmployeeAsync(CreateEmployeeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) 
                throw new Exception("Phone Number is Required");

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId,
                RoleId = dto.RoleId,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
       
            await _repo.AddEmployeeAsync(employee);


        }
        public async Task UpdateEmployeeAsync(EmployeeDto dto)
        {
            if (dto.Id <= 0)
                throw new Exception("Invalid Employee ID");

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");
         

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new Exception("Phone Number is Required");

            var employee = await _repo.GetEmployeeByIdAsync(dto.Id);

            if (employee is null)
                throw new Exception("Employe not found");
         
            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Email = dto.Email;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.HireDate = dto.HireDate;
            employee.Salary = dto.Salary;
            employee.DepartmentId = dto.DepartmentId;
            employee.RoleId = dto.RoleId;

            await _repo.UpdateEmployeeAsync(employee);
        }
        public async Task ActiveEmployeeByIdAsync(int id)
        {
           await _repo.ActiveEmployeeByIdAsync(id);

        }
        public async Task InActiveEmployeeByIdAsync(int id)
        {
           await _repo.InActiveEmployeeByIdAsync(id);
        }
    }
}
