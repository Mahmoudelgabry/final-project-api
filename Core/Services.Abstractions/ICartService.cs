using Shared.DTOs.Cart;

namespace Services.Abstractions
{
    public interface ICartService
    {
        Task AddToCartAsync(int userId, AddToCartDto dto);

        Task<CartDto> GetCartAsync(int userId);

        Task UpdateQuantityAsync(int userId, int itemId, UpdateCartItemDto dto);

        Task DeleteItemAsync(int userId, int itemId);

        Task<CartSummaryDto> GetSummaryAsync(int userId);
    }
}