namespace Shared.DTOs.Order
{
    public class OrderResultDto
    {
        public int Id { get; set; }

    public decimal TotalPrice { get; set; }

        public decimal Tax { get; set; }

        public decimal ShippingCost { get; set; } // 🧾 NEW

        public string PaymentMethod { get; set; } // 🧾 NEW

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
