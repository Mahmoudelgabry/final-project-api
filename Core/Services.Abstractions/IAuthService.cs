using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<object> LoginAsync(LoginDto dto);
        Task LogoutAsync(int userId);
        Task<string> RefreshTokenAsync(string refreshToken);
        Task ForgotPasswordAsync(string email);          
        Task ResetPasswordAsync(ResetPasswordDto dto);   
    }
}
