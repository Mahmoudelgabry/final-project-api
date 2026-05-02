using System.Collections.Generic;

namespace Shared.DTOs.Cart
{
    public class CartDto
    {
        public IEnumerable<CartItemDto> Items { get; set; }

        public decimal Subtotal { get; set; }
    }
}