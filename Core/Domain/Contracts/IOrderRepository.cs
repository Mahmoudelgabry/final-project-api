using Domain.Models;

namespace Domain.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();

    Task<Order?> GetOrderWithItemsAsync(int orderId);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        // 💣 Idempotency (NEW)
        Task<Order?> GetByRequestIdAsync(string requestId, int userId);

        Task AddAsync(Order order);
    }

}
