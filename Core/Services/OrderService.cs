using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs.Order;
using Shareds.Exceptions;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task<OrderResultDto> PlaceOrderAsync(
            int userId,
            CreateOrderDto dto)
        {
            // 🔥 Idempotency
            if (!string.IsNullOrEmpty(dto.RequestId))
            {
                var existingOrder = await _unitOfWork
                    .OrderRepository
                    .GetByRequestIdAsync(dto.RequestId, userId);

                if (existingOrder != null)
                {
                    return _mapper.Map<OrderResultDto>(existingOrder);
                }
            }

            List<OrderItem> items = new();

            // ============================
            // 🟡 تجهيز Items
            // ============================
            if (dto.Source.ToLower() == "cart")
            {
                var cart = await _unitOfWork
                    .CartRepository
                    .GetCartWithItemsAsync(userId);

                if (cart == null || !cart.Items.Any())
                    throw new Exception("Cart is empty");

                foreach (var item in cart.Items)
                {
                    // Product
                    if (item.Product != null)
                    {
                        if (!item.Product.InStock)
                            throw new Exception(
                                $"Product '{item.Product.Name}' is out of stock");

                        items.Add(new OrderItem
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            PriceAtPurchase = item.Product.Price
                        });
                    }

                    // GhostCraft
                    else if (item.GhostCraftOrder != null)
                    {
                        items.Add(new OrderItem
                        {
                            GhostCraftOrderId = item.GhostCraftOrderId,
                            Quantity = item.Quantity,
                            PriceAtPurchase = item.GhostCraftOrder.Price
                        });
                    }
                }
            }
            else if (dto.Source.ToLower() == "buynow")
            {
                if (dto.ProductId == null || dto.Quantity == null)
                    throw new Exception("Invalid BuyNow data");

                var product = await _unitOfWork
                    .GetRepository<Product, int>()
                    .GetAsync(dto.ProductId.Value);

                if (product == null)
                    throw new NotFoundException("Product not found");

                if (!product.InStock)
                    throw new Exception(
                        $"Product '{product.Name}' is out of stock");

                items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = dto.Quantity.Value,
                    PriceAtPurchase = product.Price
                });
            }
            else
            {
                throw new Exception("Invalid source");
            }

            // ============================
            // 🔥 الحساب
            // ============================
            decimal subtotal = items.Sum(i =>
                i.PriceAtPurchase * i.Quantity);

            decimal tax = subtotal * 0.08m;

            decimal shipping = subtotal > 50
                ? 0
                : 5.99m;

            decimal total = subtotal + tax + shipping;

            // ============================
            // 🟢 إنشاء Order
            // ============================
            var order = new Order
            {
                UserId = userId,
                Address = dto.Address,

                TotalPrice = Math.Round(total, 2),

                Tax = Math.Round(tax, 2),

                ShippingCost = shipping,

                OrderStatus = OrderStatus.Pending,

                PaymentStatus = PaymentStatus.Pending,

                PaymentMethod =
                    (PaymentMethod)dto.PaymentMethodId,

                RequestId = dto.RequestId,

                CreatedAt = DateTime.UtcNow,

                OrderItems = items
            };

            await _unitOfWork
                .OrderRepository
                .AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            // 🟢 Cash Handling
            if (dto.PaymentMethodId == 2)
            {
                order.OrderStatus = OrderStatus.Confirmed;

                await _unitOfWork.SaveChangesAsync();
            }

            // 🟢 Clear Cart
            if (dto.Source.ToLower() == "cart")
            {
                var cart = await _unitOfWork
                    .CartRepository
                    .GetCartWithItemsAsync(userId);

                if (cart != null)
                {
                    cart.Items.Clear();

                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return _mapper.Map<OrderResultDto>(order);
        }

        // 🚫 Cancel Order
        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _unitOfWork
                .GetRepository<Order, int>()
                .GetAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            if (order.PaymentStatus == PaymentStatus.Paid)
                throw new Exception("Cannot cancel paid order");

            order.OrderStatus = OrderStatus.Cancelled;

            await _unitOfWork.SaveChangesAsync();
        }

        // 🟣 User Orders
        public async Task<IEnumerable<OrderResultDto>>
            GetUserOrdersAsync(int userId)
        {
            var orders = await _unitOfWork
                .OrderRepository
                .GetOrdersByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        // 🔵 Order Details
        public async Task<OrderDto>
            GetOrderDetailsAsync(int orderId)
        {
            var order = await _unitOfWork
                .OrderRepository
                .GetOrderWithItemsAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            return _mapper.Map<OrderDto>(order);
        }
    }
}