using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs.Cart;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController(IServiceManager serviceManager) : ControllerBase
    {
        // 🔥 Helper
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        // 🟢 Add to Cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var userId = GetUserId();

            await serviceManager.CartService.AddToCartAsync(userId, dto);

            return Ok(new { message = "Item added to cart" });
        }

        // 🟡 Get Cart
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var userId = GetUserId();

            var cart = await serviceManager.CartService.GetCartAsync(userId);

            return Ok(cart);
        }

        // 🔵 Update Quantity
        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateQuantity(int itemId, UpdateCartItemDto dto)
        {
            var userId = GetUserId();

            await serviceManager.CartService.UpdateQuantityAsync(userId, itemId, dto);

            return NoContent();
        }

        // 🔴 Delete Item
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            var userId = GetUserId();

            await serviceManager.CartService.DeleteItemAsync(userId, itemId);

            return NoContent();
        }

        // 🟣 Summary
        [HttpGet("summary")]
        public async Task<ActionResult<CartSummaryDto>> GetSummary()
        {
            var userId = GetUserId();

            var summary = await serviceManager.CartService.GetSummaryAsync(userId);

            return Ok(summary);
        }
    }
}