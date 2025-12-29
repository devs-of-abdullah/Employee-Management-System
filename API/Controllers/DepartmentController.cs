using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) { _departmentService = departmentService; }

        [HttpGet("getdepartments")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllDepartmentsAsync());
        }

        [HttpPost("createdepartment")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            try
            {
                await _departmentService.CreateDepartment(dto);
            }
            catch (Exception ex) { return NotFound(ex.Message); }

            return Ok(dto);
        }

        [HttpGet("getdepartment/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var department = await _departmentService.GetDepartment(id);
            if (department == null) return NotFound();
            return Ok(department);
        }
        [HttpDelete("deletedepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departmentService.DeleteDepartment(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("updatedepartment/{Id}")]
        public async Task<IActionResult> UpdateDepartment(int Id, UpdateDepartmentDto dto)
        {
            try
            {
                await _departmentService.UpdateDepartment(Id, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(dto);
        }

        [HttpPost("addemployeetodepartment/{id}/{employeId}")]
        public async Task<IActionResult> AddEmployeeToRole(int id, int employeId)
        {
            try
            {
                await _departmentService.AddEmployeeToDepartment(id, employeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("removeemployeefromdepartment/{id}/{employeId}")]
        public async Task<IActionResult> RemoveEmployeeFromDepartment(int id, int employeId)
        {
            try
            {
                await _departmentService.RemoveEmployeeFromDepartment(id, employeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
