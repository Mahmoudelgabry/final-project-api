namespace Shared
{
    public class SavedPaymentDto
    {
        public int Id { get; set; }

        public string Last4Digits { get; set; }

        public string Brand { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}