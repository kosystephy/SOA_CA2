using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Enums;
using SOA_CA2_E_Commerce.Helpers;
using SOA_CA2_E_Commerce.Services;
using System.Security.Claims;

namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                var apiKey = await _authService.Register(registerDto);
                return Ok(new { ApiKey = apiKey });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                // Updated to return User_Id in addition to JwtToken, RefreshToken, and ApiKey
                var (jwtToken, refreshToken, apiKey, userId,role) = await _authService.Login(loginDto);
                return Ok(new
                {
                    JwtToken = jwtToken,
                    RefreshToken = refreshToken,
                    ApiKey = apiKey,
                    UserId = userId,
                    Role = role
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string oldRefreshToken)
        {
            try
            {
                var newJwtToken = await _authService.RefreshToken(oldRefreshToken);
                return Ok(new { JwtToken = newJwtToken });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] string refreshToken)
        {
            try
            {
                await _authService.RevokeRefreshToken(refreshToken);
                return Ok(new { Message = "Refresh token revoked successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    
        [HttpGet("api-key")]
        public async Task<IActionResult> GetApiKey()
        {
            try
            {
                // Get the current user's email from the JWT claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                if (string.IsNullOrEmpty(userEmail))
                    return Unauthorized(new { Message = "Unable to identify the user." });

                var apiKey = await _authService.GetApiKeyForUser(userEmail);

                return Ok(new { ApiKey = apiKey });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
