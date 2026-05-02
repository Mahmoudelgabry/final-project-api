using Shared.DTOs.Order;

namespace Services.Abstractions
{
    public interface IAdminOrderService
    {
        Task<IEnumerable<AdminOrderDto>> GetAllOrdersAsync();

    Task<AdminOrderDto> GetOrderByIdAsync(int orderId);

        Task UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto);
    }

}
