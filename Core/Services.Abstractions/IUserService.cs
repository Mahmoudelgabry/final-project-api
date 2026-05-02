using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task UpdateUserStatus(int userId, bool isActive);
        Task<UserProfileDto> GetProfileAsync(int userId);

        Task UpdateProfileAsync(int userId, UpdateProfileDto dto);

        Task ChangePasswordAsync(int userId, ChangePasswordDto dto);
    }
}
