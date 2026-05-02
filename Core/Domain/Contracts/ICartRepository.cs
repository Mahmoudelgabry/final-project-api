using Domain.Models;

namespace Domain.Contracts
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartWithItemsAsync(int userId);
    }
}