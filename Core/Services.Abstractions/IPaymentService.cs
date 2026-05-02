using Shared;

namespace Services.Abstractions
{
    public interface IPaymentService
    {
        Task<int> StartPaymentAsync(string userId, StartPaymentDto dto);

        Task<PaymentResultDto> ConfirmPaymentAsync(string userId, int orderId, ConfirmPaymentDto dto);

        Task<PaymentResultDto> PayWithSavedCardAsync(string userId, int orderId, int savedCardId);

        Task<IEnumerable<SavedPaymentDto>> GetSavedMethodsAsync(string userId);

        Task DeleteSavedMethodAsync(string userId, int id);
    }
}