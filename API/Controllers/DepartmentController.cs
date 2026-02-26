using Business.Interfaces;
using DTO.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/departments")]
[Authorize]
public class DepartmentController : ControllerBase
{
    readonly IDepartmentService _service;
    public DepartmentController(IDepartmentService service) { _service = service; }

    [HttpGet]
    [ProducesResponseType(typeof(List<DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var department = await _service.GetByIdAsync(id);
        return department == null ? NotFound("Department not found.") : Ok(department);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:int}/name")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateDepartmentNameDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.UpdateNameAsync(id, dto);
        return NoContent();
    }

    [HttpPut("{id:int}/description")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateDescription(int id, [FromBody] UpdateDepartmentDescriptionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.UpdateDescriptionAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id:int}/employees/{employeeId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddEmployee(int id, int employeeId)
    {
        await _service.AddEmployeeAsync(id, employeeId);
        return NoContent();
    }

    [HttpDelete("{id:int}/employees/{employeeId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveEmployee(int id, int employeeId)
    {
        await _service.RemoveEmployeeAsync(id, employeeId);
        return NoContent();
    }
}