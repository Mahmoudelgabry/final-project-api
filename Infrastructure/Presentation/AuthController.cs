using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System.Security.Claims;


namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            
            await serviceManager.AuthService.RegisterAsync(dto);
            return Ok("User created successfully");
            
            
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            var result = await serviceManager.AuthService.LoginAsync(dto);
            return Ok( result );
        }

        [Authorize]
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
        {
            await serviceManager.AuthService.LogoutAsync(dto.RefreshToken);

            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDto dto)
        {
            var token = await serviceManager.AuthService
                .RefreshTokenAsync(dto.RefreshToken);

            return Ok(new { token });
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await serviceManager.AuthService.ForgotPasswordAsync(dto.Email);
            return Ok("Reset process started");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            await serviceManager.AuthService.ResetPasswordAsync(dto);
            return Ok("Password reset successfully");
        }
    }
}
