using System.Collections.Generic;

namespace Domain.Models
{
    public class Cart : BaseEntity<int>
    {
        public int UserId { get; set; }

        // Navigation
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}