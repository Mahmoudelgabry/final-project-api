using AutoMapper;
using Domain.Models;
using Shared;


namespace Services.Profiles
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteProductDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))

                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Product.ImageUrl))

                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Product.Price))

                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Product.Category.Name));

        }
    }
}
