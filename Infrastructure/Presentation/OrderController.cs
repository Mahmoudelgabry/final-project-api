using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs.Order;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

    public OrderController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        // ============================
        // 🔹 Place Order
        // ============================
        [HttpPost("place")]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
        {
            var userId = GetUserId();

            var result = await _serviceManager.OrderService.PlaceOrderAsync(userId, dto);

            return Ok(result);
        }

        // ============================
        // 🔹 Cancel Order
        // ============================
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _serviceManager.OrderService.CancelOrderAsync(id);

            return Ok(new
            {
                message = "Order cancelled successfully"
            });
        }

        // ============================
        // 🔹 Get My Orders
        // ============================
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserId();

            var orders = await _serviceManager.OrderService.GetUserOrdersAsync(userId);

            return Ok(orders);
        }

        // ============================
        // 🔹 Get Order Details
        // ============================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _serviceManager.OrderService.GetOrderDetailsAsync(id);

            return Ok(order);
        }
    }

}
