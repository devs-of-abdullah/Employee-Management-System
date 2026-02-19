using Business.Interfaces;
using Data.Interfaces;
using Entities;
using DTO.Employee;
namespace Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo) 
        {
            _repo = repo;
        }
        public async Task<List<ReadEmployeeDto>> GetAllAsync()
        {
            return null;
        }
        public async Task<ReadEmployeeDto> GetByIdAsync(int id)
        {
            return null;
        }
        public  async Task AddAsync(CreateEmployeeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber)) 
                throw new Exception("Phone Number is Required");

            var employee = new EmployeeEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                HireDate = dto.HireDate,
                Salary = dto.Salary,
            };
       
            await _repo.AddAsync(employee);


        }
        public async Task UpdateAsync()
        {

          

        }
        public async Task ActivateAsync(int id)
        {
           await _repo.SetActiveAsync(id,true);

        }
        public async Task DeactivateAsync(int id)
        {
           await _repo.SetActiveAsync(id,false);
        }
        public async Task DeleteAsync(int id) 
        {
            await _repo.DeleteAsync(id);
        }
    }
}
