using Shared.DTOs.Order;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResultDto> PlaceOrderAsync(int userId, CreateOrderDto dto);

    Task CancelOrderAsync(int orderId);

        Task<IEnumerable<OrderResultDto>> GetUserOrdersAsync(int userId);

        Task<OrderDto> GetOrderDetailsAsync(int orderId);
    }

}
