using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }

        public string Address { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal ShippingCost { get; set; }

        public string? RequestId { get; set; }

        public DateTime CreatedAt { get; set; }

        // 🔥 FIX: rename from Items → OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}