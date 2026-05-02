using Domain.Models;

namespace Domain.Contracts
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> GetByUserIdAsync(int userId);
        Task AddAsync(UserProfile profile);
        void Update(UserProfile profile);
    }
}