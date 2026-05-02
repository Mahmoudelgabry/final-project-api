namespace Shared.DTOs.Order
{
    public class AdminOrderDto
    {
        public int Id { get; set; }

    public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentMethod { get; set; } // 🧾 NEW

        public DateTime CreatedAt { get; set; }
    }

}
