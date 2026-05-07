using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared.DTOs.Cart;
using Shareds.Exceptions;

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
            if (dto.ProductId == null && dto.GhostCraftOrderId == null)
                throw new BadRequestException("Item is required");

            if (dto.ProductId != null && dto.GhostCraftOrderId != null)
                throw new BadRequestException("Only one item type allowed");
            if (dto.Quantity <= 0)
                throw new BadRequestException("Invalid quantity");
            var cart = await GetOrCreateCart(userId);

            CartItem? existingItem = null;

            // Product
            if (dto.ProductId != null)
            {
                existingItem = cart.Items
                    .FirstOrDefault(i => i.ProductId == dto.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        ProductId = dto.ProductId.Value,
                        Quantity = dto.Quantity
                    });
                }
            }

            // GhostCraft
            if (dto.GhostCraftOrderId != null)
            {
                var ghostCraftRepo =
                    _unitOfWork.GetRepository<GhostCraftOrder, int>();

                var ghostCraft =
                    await ghostCraftRepo.GetAsync(dto.GhostCraftOrderId.Value);

                if (ghostCraft == null)
                    throw new NotFoundException("GhostCraft not found");


                existingItem = cart.Items
                    .FirstOrDefault(i =>
                        i.GhostCraftOrderId == dto.GhostCraftOrderId);

                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        GhostCraftOrderId = dto.GhostCraftOrderId.Value,
                        Quantity = dto.Quantity
                    });
                }
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
                throw new NotFoundException("Item not found");

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
                throw new NotFoundException("Item not found");

            cart.Items.Remove(item);

            await _unitOfWork.SaveChangesAsync();
        }

        // 🟣 Summary
        public async Task<CartSummaryDto> GetSummaryAsync(int userId)
        {
            var cart = await GetOrCreateCart(userId);

            var subtotal = cart.Items.Sum(i =>
              (i.Product != null
                 ? i.Product.Price
                 : i.GhostCraftOrder.Price)
              * i.Quantity);

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