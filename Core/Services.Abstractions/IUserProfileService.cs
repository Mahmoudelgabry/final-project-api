using Shared.DTOs.UserProfile;

namespace Services.Abstractions
{
    public interface IUserProfileService
    {
        Task<GetUserProfileDto> GetUserProfileAsync(int userId);

        Task UpdateUserProfileAsync(int userId, UpdateUserProfileDto dto);
    }
}