using AutoMapper;
using Domain.Models;
using Shared.DTOs.Cart;

namespace Services.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            // 🔥 CartItem → CartItemDto
            CreateMap<CartItem, CartItemDto>()
                .ForMember(d => d.CartItemId,
                    opt => opt.MapFrom(s => s.Id))

                .ForMember(d => d.Name,
                    opt => opt.MapFrom(s => s.Product.Name))

                .ForMember(d => d.ImageUrl,
                    opt => opt.MapFrom(s => s.Product.ImageUrl))

                .ForMember(d => d.Price,
                    opt => opt.MapFrom(s => s.Product.Price))

                .ForMember(d => d.Total,
                    opt => opt.MapFrom(s => s.Product.Price * s.Quantity));


            // 🔥 Cart → CartDto
            CreateMap<Cart, CartDto>()
                .ForMember(d => d.Subtotal,
                    opt => opt.MapFrom(s =>
                        s.Items.Sum(i => i.Product.Price * i.Quantity)
                    ));
        }
    }
}