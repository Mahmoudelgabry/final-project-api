using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs.Order;
using Shareds.Exceptions;

namespace Services
{
    public class AdminOrderService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAdminOrderService
    {
        public async Task<IEnumerable<AdminOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllOrdersAsync();
            return _mapper.Map<IEnumerable<AdminOrderDto>>(orders);
        }

    public async Task<AdminOrderDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            return _mapper.Map<AdminOrderDto>(order);
        }

        public async Task UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto dto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var newStatus))
                throw new Exception("Invalid status");

            // 🚫 منع التعديل على الحالات النهائية
            if (order.OrderStatus == OrderStatus.Cancelled ||
                order.OrderStatus == OrderStatus.Delivered)
            {
                throw new Exception("Cannot modify completed order");
            }

            // 🚫 منع Cancel لو Paid
            if (newStatus == OrderStatus.Cancelled &&
                order.PaymentStatus == PaymentStatus.Paid)
            {
                throw new Exception("Cannot cancel a paid order");
            }

            order.OrderStatus = newStatus;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
