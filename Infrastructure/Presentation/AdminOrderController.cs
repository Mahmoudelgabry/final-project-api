using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs.Order;

namespace API.Controllers
{
    [ApiController]
    [Route("api/admin/orders")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

    public AdminOrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // ============================
        // 🔹 Get All Orders
        // ============================
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _serviceManager.AdminOrderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        // ============================
        // 🔹 Get Order By Id
        // ============================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _serviceManager.AdminOrderService.GetOrderByIdAsync(id);

            return Ok(order);
        }

        // ============================
        // 🔹 Update Order Status
        // ============================
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, UpdateOrderStatusDto dto)
        {
            await _serviceManager.AdminOrderService.UpdateOrderStatusAsync(id, dto);

            return Ok(new
            {
                message = "Order status updated successfully"
            });
        }
    }

}
