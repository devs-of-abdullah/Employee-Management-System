using Business.Interfaces;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    readonly IUserService _userService;
    readonly IAuthorizationService _authorizationService;

    public UsersController(IUserService userService, IAuthorizationService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(typeof(ReadUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID.");

        var authResult = await _authorizationService.AuthorizeAsync(User, id, "UserOwnerOrAdmin");
        if (!authResult.Succeeded)
            return User.Identity?.IsAuthenticated == true ? Forbid() : Unauthorized();

        var user = await _userService.GetByIdAsync(id);
        return user == null ? NotFound("User not found.") : Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    [ProducesResponseType(typeof(ReadUserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _userService.GetByEmailAsync(dto.Email);
        if (existing != null)
            return Conflict("A user with this email already exists.");

        var createdId = await _userService.CreateAsync(dto);
        var createdUser = await _userService.GetByIdAsync(createdId);

        return CreatedAtRoute("GetUserById", new { id = createdId }, createdUser);
    }

    [Authorize]
    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] UpdateUserPasswordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        await _userService.UpdatePasswordAsync(userId, dto);
        return NoContent();
    }

    [Authorize]
    [HttpPut("change-email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangeEmail([FromBody] UpdateUserEmailDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        await _userService.UpdateEmailAsync(userId, dto);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("self")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SelfDelete([FromBody] SoftUserDeleteDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return Unauthorized();

        await _userService.SoftDeleteAsync(userId, dto);
        return NoContent();
    }

    [Authorize(Roles = "admin,superAdmin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdminDelete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID.");

        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound("User not found.");

        await _userService.UpdateRoleAsync(id, new UpdateUserRoleDTO { NewRole = user.Role }); 
                                                                                              
        await _userService.SoftDeleteAsync(id, new SoftUserDeleteDTO { CurrentPassword = string.Empty });
        return NoContent();
    }

    [Authorize(Roles = "superAdmin")]
    [HttpPut("{id:int}/role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _userService.UpdateRoleAsync(id, dto);
        return NoContent();
    }
}