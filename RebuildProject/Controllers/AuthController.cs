using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CharityProject.Controllers
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

        // =============================
        // REGISTER
        // =============================
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(result);
        }

        // =============================
        // LOGIN
        // =============================
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized("Invalid credentials or user is banned.");

            return Ok(result);
        }

        // =============================
        // REFRESH TOKEN
        // =============================
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (result == null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }
    }
}
