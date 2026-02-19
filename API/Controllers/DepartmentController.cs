using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        { 
            _service = service; 
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          return Ok(await _service.GetAllAsync());
        }

     
     
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var department = await _service.GetByIdAsync(id);
            return department == null ? NotFound() : Ok(department);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        
       

        [Authorize]
        [HttpPost("{id:int}/employees{employeId:int}")]
        public async Task<IActionResult> AddEmployee(int id, int employeId)
        {
           
           await _service.AddEmployeeAsync(id, employeId);
               
            return NoContent();
            
            
        }

        [Authorize]
        [HttpDelete("{id:int}/employees{employeId:int}")]
        public async Task<IActionResult> RemoveEmployee(int id, int employeId)
        {
            await _service.RemoveEmployeeAsync(id,employeId);
            return NoContent();
        }
    }
}
