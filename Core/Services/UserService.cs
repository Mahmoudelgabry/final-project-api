using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService(IUnitOfWork _unitOfWork, IMapper _mapper) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork
                .GetRepository<User, int>()
                .GetAllAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task UpdateUserStatus(int userId, bool isActive)
        {
            var user = await _unitOfWork
                .GetRepository<User, int>()
                .GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            user.IsActive = isActive;

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var user = await _unitOfWork
                .GetRepository<User, int>()
                .GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            var repo = _unitOfWork.GetRepository<User, int>();

            var user = await repo.GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            repo.Update(user);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var repo = _unitOfWork.GetRepository<User, int>();

            var user = await repo.GetAsync(userId);

            if (user == null)
                throw new NotFoundException("User not found");

            var isValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash);

            if (!isValid)
                throw new BadRequestException("Wrong current password");

            if (dto.NewPassword.Length < 6)
                throw new BadRequestException("Password must be at least 6 characters");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
