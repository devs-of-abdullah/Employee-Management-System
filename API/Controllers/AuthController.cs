
    using Business.Interfaces;
    using DTO.Auth;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.RateLimiting;
    using System.Security.Claims;

    [ApiController]
    [Route("api/auth")]
    [EnableRateLimiting("AuthLimiter")]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        public AuthController(IAuthService authService) { _authService = authService; }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokens = await _authService.LoginAsync(dto);
            if (tokens == null)
                return Unauthorized("Invalid credentials.");

            return Ok(tokens);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(TokenResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokens = await _authService.RefreshTokenAsync(dto);
            if (tokens == null)
                return Unauthorized("Invalid or expired refresh token.");

            return Ok(tokens);
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
                return Unauthorized();

            await _authService.LogoutAsync(userId, dto.RefreshToken);
            return NoContent();
        }
    }