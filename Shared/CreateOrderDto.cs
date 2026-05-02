namespace Shared.DTOs.Order
{
    public class CreateOrderDto
    {
        public string Address { get; set; }

    public string Source { get; set; } // Cart أو BuyNow

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int PaymentMethodId { get; set; }

        public string? RequestId { get; set; } // 💣 NEW
    }

}
