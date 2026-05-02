using Domain.Contracts;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthService(IUnitOfWork _unitOfWork) : IAuthService
    {
        // ================= REGISTER =================
        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _unitOfWork.UserRepository
                .GetByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new BadRequestException("Email already exists");

            if (dto.Password != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match");

            if (dto.Password.Length < 6)
                throw new BadRequestException("Password must be at least 6 characters");

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<User, int>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        // ================= LOGIN =================
        public async Task<object> LoginAsync(LoginDto dto)
        {
            var user = await _unitOfWork.UserRepository
                .GetByEmailAsync(dto.Email);

            if (user == null)
                throw new BadRequestException("Invalid email or password");

            if (!user.IsActive)
                throw new BadRequestException("User is blocked 🚫");

            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                throw new BadRequestException("Invalid email or password");

            var token = GenerateToken(user);

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            return new
            {
                token,
                refreshToken = user.RefreshToken
            };
        }

        // ================= LOGOUT =================
        public async Task LogoutAsync(int userId)
        {
            var user = await _unitOfWork
                .GetRepository<User, int>()
                .GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            user.RefreshToken = null;

            await _unitOfWork.SaveChangesAsync();
        }

        // ================= REFRESH TOKEN =================
        public async Task<string> RefreshTokenAsync(string refreshToken)
        {
            var users = await _unitOfWork
                .GetRepository<User, int>()
                .GetAllAsync();

            var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user == null)
                throw new BadRequestException("Invalid refresh token");

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new BadRequestException("Refresh token expired");

            return GenerateToken(user);
        }

        // ================= FORGOT PASSWORD =================
        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _unitOfWork.UserRepository
                .GetByEmailAsync(email);

            if (user == null)
                throw new NotFoundException("User not found");
        }

        // ================= RESET PASSWORD =================
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _unitOfWork.UserRepository
                .GetByEmailAsync(dto.Email);

            if (user == null)
                throw new NotFoundException("User not found");

            if (dto.NewPassword != dto.ConfirmPassword)
                throw new BadRequestException("Passwords do not match");

            if (dto.NewPassword.Length < 6)
                throw new BadRequestException("Password must be at least 6 characters");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _unitOfWork.SaveChangesAsync();
        }

        // ================= HELPERS =================

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("THIS_IS_A_SUPER_SECRET_KEY_123456789"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}