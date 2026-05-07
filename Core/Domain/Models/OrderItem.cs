namespace Domain.Models
{
    public class OrderItem : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int? ProductId { get; set; }
        public Product? Product { get; set; } 

        public int? GhostCraftOrderId { get; set; }
        public GhostCraftOrder? GhostCraftOrder { get; set; }

        public int Quantity { get; set; }

        public decimal PriceAtPurchase { get; set; }
    }

}
