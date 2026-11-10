using LibraryManagementSystem.DTOs;
using LibraryManagementSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.IsSuccess) return Unauthorized(result);
            return Ok(result);
        }

        // 💡 NEW ENDPOINT: Handles incoming client requests to rotate expired access tokens
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] AuthResponseDto request)
        {
            if (request == null) return BadRequest("Invalid request payload");

            var result = await _authService.RefreshTokenAsync(request);
            if (!result.IsSuccess) return BadRequest(result);

            return Ok(result);
        }
    }
}
