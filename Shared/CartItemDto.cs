namespace Shared.DTOs.Cart
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int? ProductId { get; set; }
        public int? GhostCraftOrderId { get; set; }

        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total { get; set; }

    }
}