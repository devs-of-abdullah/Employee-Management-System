
using Business.Interfaces;
using DTO.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/roles")]
[Authorize]
public class RoleController : ControllerBase
{
    readonly IRoleService _service;
    public RoleController(IRoleService service) { _service = service; }

    [HttpGet]
    [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _service.GetByIdAsync(id);
        return role == null ? NotFound("Role not found.") : Ok(role);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:int}/name")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateRoleNameDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.UpdateNameAsync(id, dto);
        return NoContent();
    }

    [HttpPut("{id:int}/description")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateDescription(int id, [FromBody] UpdateRoleDescriptionDto dto)
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

    // ✅ Fixed: route was "{id:int}/employees{employeId:int}" missing slash separator
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