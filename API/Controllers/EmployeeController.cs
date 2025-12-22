using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities.DTOs;
namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController: ControllerBase
    {
        readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllEmployeesAsync());
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _employeeService.GetEmployeeByIdAsync(id);
            return e == null ? NotFound() : Ok(e);  

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto) 
        {
         
            await _employeeService.AddEmployeeAsync(dto);
            return Ok(dto);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDto employee) 
        {
            if (id != employee.Id) return BadRequest();
            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent();
        }
        [HttpPatch("active/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            await _employeeService.ActiveEmployeeByIdAsync(id);
            return NoContent();
        }
        [HttpPatch("inactive/{id}")]
        public async Task<IActionResult> InActive(int id)
        {
            await _employeeService.ActiveEmployeeByIdAsync(id);
            return NoContent();
        }


    }
}
