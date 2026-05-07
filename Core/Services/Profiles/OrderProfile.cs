using AutoMapper;
using Domain.Models;
using Shared.DTOs.Order;

namespace Shared.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // 🔹 OrderItem → DTO
            
                CreateMap<OrderItem, OrderItemDto>()
                  .ForMember(dest => dest.Name,
                     opt => opt.MapFrom(src =>
                       src.Product != null
                         ? src.Product.Name
                         : src.GhostCraftOrder.DishDescription))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.PriceAtPurchase))
                .ForMember(dest => dest.Quantity,
                     opt => opt.MapFrom(src => src.Quantity));

            // 🔹 Order → Result DTO
            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.ShippingCost,
                    opt => opt.MapFrom(src => src.ShippingCost))
                .ForMember(dest => dest.PaymentMethod,
                    opt => opt.MapFrom(src => src.PaymentMethod.ToString()));

            // 🔹 Order → Full Details
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderStatus,
                    opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.PaymentMethod,
                    opt => opt.MapFrom(src => src.PaymentMethod.ToString()))
                .ForMember(dest => dest.Items,
                    opt => opt.MapFrom(src => src.OrderItems)); // 🔥 FIX

            // 🔥 Admin DTO
            CreateMap<Order, AdminOrderDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.OrderStatus,
                    opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.PaymentStatus,
                    opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.PaymentMethod,
                    opt => opt.MapFrom(src => src.PaymentMethod.ToString()));
        }
    }
}