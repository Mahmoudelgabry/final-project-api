using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ============================
        // 🟡 Start Payment
        // ============================
        public async Task<int> StartPaymentAsync(string userId, StartPaymentDto dto)
        {
            if (string.IsNullOrEmpty(dto.PaymentMethod))
                throw new Exception("Payment method is required");

            var method = dto.PaymentMethod.ToLower();

            return method switch
            {
                "card" => 1,
                "cash" => 2,
                _ => throw new Exception("Invalid payment method")
            };
        }

        // ============================
        // 🟢 Confirm Payment (Card)
        // ============================
        public async Task<PaymentResultDto> ConfirmPaymentAsync(string userId, int orderId, ConfirmPaymentDto dto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            if (order.UserId != int.Parse(userId))
                throw new Exception("Unauthorized");

            // 💳 Duplicate Payment Protection
            if (order.PaymentStatus == PaymentStatus.Paid)
                throw new Exception("Order already paid");

            if (string.IsNullOrEmpty(dto.CardNumber) ||
                string.IsNullOrEmpty(dto.CVV))
            {
                throw new Exception("Invalid card data");
            }

            // ============================
            // 🔥 Detect Card Brand
            // ============================
            string brand = dto.CardNumber.StartsWith("4") ? "Visa" :
                           dto.CardNumber.StartsWith("5") ? "MasterCard" :
                           dto.CardNumber.StartsWith("3") ? "Amex" :
                           "Unknown";

            // ============================
            // 🔥 Create Transaction
            // ============================
            var transactionId = Guid.NewGuid().ToString();

            var payment = new Payment
            {
                UserId = userId,
                OrderId = order.Id,
                Amount = order.TotalPrice,
                PaymentMethod = PaymentMethod.Card,
                Status = PaymentStatus.Paid,
                TransactionId = transactionId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<Payment, int>().AddAsync(payment);

            // ============================
            // 🔥 Save Card (لو مطلوب)
            // ============================
            if (dto.SavePayment)
            {
                var last4 = dto.CardNumber.Substring(dto.CardNumber.Length - 4);

                var savedCard = new SavedPaymentMethod
                {
                    UserId = userId,
                    Last4Digits = last4,
                    Brand = brand,
                    ExpiryMonth = dto.ExpiryMonth,
                    ExpiryYear = dto.ExpiryYear,
                    Token = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork
                    .GetRepository<SavedPaymentMethod, int>()
                    .AddAsync(savedCard);
            }

            // ============================
            // 🔥 Update Order
            // ============================
            order.PaymentStatus = PaymentStatus.Paid;
            order.OrderStatus = OrderStatus.Confirmed;

            await _unitOfWork.SaveChangesAsync();

            return new PaymentResultDto
            {
                OrderId = order.Id,
                PaymentStatus = order.PaymentStatus.ToString(),
                OrderStatus = order.OrderStatus.ToString()
            };
        }

        // ============================
        // 🟣 Pay With Saved Card
        // ============================
        public async Task<PaymentResultDto> PayWithSavedCardAsync(string userId, int orderId, int savedCardId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            if (order.UserId != int.Parse(userId))
                throw new Exception("Unauthorized");

            if (order.PaymentStatus == PaymentStatus.Paid)
                throw new Exception("Order already paid");

            var savedCard = await _unitOfWork
                .GetRepository<SavedPaymentMethod, int>()
                .GetAsync(savedCardId);

            if (savedCard == null || savedCard.UserId != userId)
                throw new Exception("Invalid saved card");

            var transactionId = Guid.NewGuid().ToString();

            var payment = new Payment
            {
                UserId = userId,
                OrderId = order.Id,
                Amount = order.TotalPrice,
                PaymentMethod = PaymentMethod.Card,
                Status = PaymentStatus.Paid,
                TransactionId = transactionId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<Payment, int>().AddAsync(payment);

            order.PaymentStatus = PaymentStatus.Paid;
            order.OrderStatus = OrderStatus.Confirmed;

            await _unitOfWork.SaveChangesAsync();

            return new PaymentResultDto
            {
                OrderId = order.Id,
                PaymentStatus = order.PaymentStatus.ToString(),
                OrderStatus = order.OrderStatus.ToString()
            };
        }

        // ============================
        // 🔵 Get Saved Cards
        // ============================
        public async Task<IEnumerable<SavedPaymentDto>> GetSavedMethodsAsync(string userId)
        {
            var cards = await _unitOfWork
                .GetRepository<SavedPaymentMethod, int>()
                .GetAllAsync();

            var userCards = cards.Where(c => c.UserId == userId);

            return _mapper.Map<IEnumerable<SavedPaymentDto>>(userCards);
        }

        // ============================
        // 🔴 Delete Saved Card
        // ============================
        public async Task DeleteSavedMethodAsync(string userId, int id)
        {
            var repo = _unitOfWork.GetRepository<SavedPaymentMethod, int>();

            var card = await repo.GetAsync(id);

            if (card == null || card.UserId != userId)
                throw new Exception("Card not found");

            repo.Delete(card);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}