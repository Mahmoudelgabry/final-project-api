using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController(IServiceManager serviceManager) : ControllerBase
    {
        private int GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null)
                throw new Exception("User ID not found");

            return int.Parse(claim.Value);
        }

        // ============================
        // 🔹 1. Start Payment
        // ============================
        [HttpPost("start")]
        public async Task<IActionResult> StartPayment(StartPaymentDto dto)
        {
            var userId = GetCurrentUserId();

            var methodId = await serviceManager.PaymentService
                .StartPaymentAsync(userId.ToString(), dto);

            return Ok(new
            {
                paymentMethod = methodId
            });
        }

        // ============================
        // 🔹 2. Confirm Payment
        // ============================
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPayment([FromQuery] int orderId, [FromBody] ConfirmPaymentDto dto)
        {
            var userId = GetCurrentUserId();

            var result = await serviceManager.PaymentService
                .ConfirmPaymentAsync(userId.ToString(), orderId, dto);

            return Ok(new
            {
                orderId = result.OrderId,
                paymentStatus = result.PaymentStatus,
                orderStatus = result.OrderStatus
            });
        }

        // ============================
        // 🔹 3. Get Saved Cards
        // ============================
        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedCards()
        {
            var userId = GetCurrentUserId();

            var cards = await serviceManager.PaymentService
                .GetSavedMethodsAsync(userId.ToString());

            return Ok(cards);
        }

        // ============================
        // 🔹 4. Pay With Saved Card
        // ============================
        [HttpPost("pay-with-saved")]
        public async Task<IActionResult> PayWithSaved([FromQuery] int orderId, [FromBody] PayWithSavedDto dto)
        {
            var userId = GetCurrentUserId();

            var result = await serviceManager.PaymentService
                .PayWithSavedCardAsync(userId.ToString(), orderId, dto.SavedCardId);

            return Ok(new
            {
                orderId = result.OrderId,
                paymentStatus = result.PaymentStatus,
                orderStatus = result.OrderStatus
            });
        }

        // ============================
        // 🔹 5. Delete Card
        // ============================
        [HttpDelete("saved/{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var userId = GetCurrentUserId();

            await serviceManager.PaymentService
                .DeleteSavedMethodAsync(userId.ToString(), id);

            return Ok(new
            {
                message = "Deleted successfully"
            });
        }
    }

}
