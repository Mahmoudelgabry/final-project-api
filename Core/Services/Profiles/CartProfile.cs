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

                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src =>
                       src.Product != null
                          ? src.Product.Name
                          : src.GhostCraftOrder.DishDescription))

               .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src =>
                       src.Product != null
                          ? src.Product.ImageUrl
                          : null))

               .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src =>
                       src.Product != null
                          ? src.Product.Price
                          : src.GhostCraftOrder.Price))

               .ForMember(dest => dest.Total,
                    opt => opt.MapFrom(src =>
                       (src.Product != null
                           ? src.Product.Price
                           : src.GhostCraftOrder.Price)
                       * src.Quantity));


            // 🔥 Cart → CartDto
            CreateMap<Cart, CartDto>()
               .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.Items))

               .ForMember(dest => dest.Subtotal,
                   opt => opt.MapFrom(src =>
                      src.Items.Sum(i =>
                        (i.Product != null
                           ? i.Product.Price
                           : i.GhostCraftOrder.Price)
                      * i.Quantity)));
        }
    }
}