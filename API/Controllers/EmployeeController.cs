using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            return employee == null ? NotFound() : Ok(employee);  

        }

     

        
        
        [Authorize]
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            await _employeeService.ActivateAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> InActive(int id)
        {
            await _employeeService.ActivateAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }

    }
}
