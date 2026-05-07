namespace Domain.Models
{
    public class CartItem : BaseEntity<int>
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public int? GhostCraftOrderId { get; set; }
        public GhostCraftOrder? GhostCraftOrder { get; set; }

        public int Quantity { get; set; }
    }
}