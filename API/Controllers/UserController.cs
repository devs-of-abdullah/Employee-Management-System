using Business.Interfaces;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
    public async Task<ActionResult<ReadUserDTO>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID.");

        var authResult = await _authorizationService.AuthorizeAsync(User, id, "UserOwnerOrAdmin");
        if (!authResult.Succeeded)
            return Forbid();

        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    [EnableRateLimiting("AuthLimiter")]

    public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdId = await _userService.CreateAsync(dto);
        if (createdId <= 0)
            return BadRequest("Error while creating user.");

        var createdUser = await _userService.GetByIdAsync(createdId);

        return CreatedAtRoute("GetUserById", new { id = createdId }, createdUser);
    }

    [Authorize]
    [HttpPut("change-password")]
    [EnableRateLimiting("AuthLimiter")]
    public async Task<IActionResult> ChangePassword([FromBody] UpdateUserPasswordDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        await _userService.UpdatePasswordAsync(userId, dto);

        return NoContent();
    }

    [Authorize]
    [HttpPut("change-email")]
    [EnableRateLimiting("AuthLimiter")]
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
  
    public async Task<IActionResult> AdminDelete(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID.");

        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound("User not found.");


        await _userService.AdminSoftDeleteAsync(id);
        return NoContent();
    }

    [Authorize(Roles = "superAdmin")]
    [HttpPut("{id:int}/role")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _userService.UpdateRoleAsync(id, dto);
        return NoContent();
    }
}