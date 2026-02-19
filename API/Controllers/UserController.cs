using Business.Interfaces;
using DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("AuthLimiter")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthorizationService _authorizationService;

    public UsersController(IUserService userService, IAuthorizationService authorizationService)
    {
        _userService = userService;
        _authorizationService = authorizationService;
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(typeof(ReadUserDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadUserDTO>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user ID.");

        var authResult = await _authorizationService.AuthorizeAsync(User, id, "UserOwnerOrAdmin");
        if (!authResult.Succeeded)
            return User.Identity?.IsAuthenticated == true ? Forbid() : Unauthorized();

        var user = await _userService.GetByIdAsync(id);

        return user is null ? NotFound("User not found.") : Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    [ProducesResponseType(typeof(ReadUserDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ReadUserDTO>> Create([FromBody] CreateUserDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userService.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return Conflict("User email already exists.");

        var createdId = await _userService.CreateAsync(dto);
        if (createdId <= 0)
            return BadRequest("Error while creating user.");

        var createdUser = await _userService.GetByIdAsync(createdId);

        return CreatedAtRoute("GetUserById", new { id = createdId }, createdUser);
    }

    [Authorize]
    [HttpPut("change-password", Name = "ChangeUserPassword")]
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
    [HttpDelete("self", Name = "SelfDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}", Name = "AdminDelete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AdminDelete(int id, [FromBody] SoftUserDeleteDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id <= 0)
            return BadRequest("Invalid user ID.");

        await _userService.SoftDeleteAsync(id, dto);

        return NoContent();
    }
}