using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs.Cart;

namespace Services
{
    public class CartService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICartService
    {
        // 🔥 Helper: يجيب الكارت أو ينشئه
        private async Task<Cart> GetOrCreateCart(int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartWithItemsAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };

                await _unitOfWork.GetRepository<Cart, int>().AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            return cart;
        }

        // 🟢 Add to Cart
        public async Task AddToCartAsync(int userId, AddToCartDto dto)
        {
            var cart = await GetOrCreateCart(userId);

            var existingItem = cart.Items
                .FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        // 🟡 Get Cart
        public async Task<CartDto> GetCartAsync(int userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartWithItemsAsync(userId);

            if (cart == null)
            {
                return new CartDto
                {
                    Items = new List<CartItemDto>(),
                    Subtotal = 0
                };
            }

            return _mapper.Map<CartDto>(cart);
        }

        // 🔵 Update Quantity
        public async Task UpdateQuantityAsync(int userId, int itemId, UpdateCartItemDto dto)
        {
            var cart = await GetOrCreateCart(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
                throw new Exception("Item not found");

            if (dto.Quantity <= 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = dto.Quantity;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        // 🔴 Delete Item
        public async Task DeleteItemAsync(int userId, int itemId)
        {
            var cart = await GetOrCreateCart(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
                throw new Exception("Item not found");

            cart.Items.Remove(item);

            await _unitOfWork.SaveChangesAsync();
        }

        // 🟣 Summary
        public async Task<CartSummaryDto> GetSummaryAsync(int userId)
        {
            var cart = await GetOrCreateCart(userId);

            var subtotal = cart.Items.Sum(i => i.Product.Price * i.Quantity);

            var tax = subtotal * 0.08m;
            var shipping = subtotal > 50 ? 0 : 5.99m;

            return new CartSummaryDto
            {
                Subtotal = subtotal,
                Tax = Math.Round(tax, 2),
                Shipping = shipping,
                Total = Math.Round(subtotal + tax + shipping, 2)
            };
        }
    }
}