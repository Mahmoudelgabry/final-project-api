namespace Shared
{
    public class PaymentResultDto
    {
        public int OrderId { get; set; }

        public string PaymentStatus { get; set; }

        public string OrderStatus { get; set; }
    }
}