namespace Shared
{
    public class ConfirmPaymentDto
    {
        public int PaymentMethodId { get; set; }

        public bool SavePayment { get; set; }

        public string CardNumber { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public string CVV { get; set; }
    }
}