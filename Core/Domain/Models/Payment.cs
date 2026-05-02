using System;

namespace Domain.Models
{
    public class Payment : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

    public string UserId { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string? TransactionId { get; set; } // 🔥 NEW

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
