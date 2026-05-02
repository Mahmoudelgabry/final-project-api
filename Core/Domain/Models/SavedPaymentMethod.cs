namespace Domain.Models
{
    public class SavedPaymentMethod : BaseEntity<int>
    {
        public string UserId { get; set; }

        public string Last4Digits { get; set; }

        public string Brand { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string Token { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}