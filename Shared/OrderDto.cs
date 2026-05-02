namespace Shared.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

    public decimal TotalPrice { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; } // 🧾 NEW

        public DateTime CreatedAt { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }

}
