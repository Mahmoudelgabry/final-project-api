using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Product → List
            CreateMap<Product, ProductListDto>()
                .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : ""))
                .ForMember(dest => dest.IsSaved,
                    opt => opt.MapFrom((src, dest, _, context) =>
                        (src.Favorites ?? new List<Favorite>())
                        .Any(f => f.UserId == (int)context.Items["UserId"])
                    ));

            // Product → Details
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : ""))
                .ForMember(dest => dest.IsSaved,
                    opt => opt.MapFrom((src, dest, _, context) =>
                        (src.Favorites ?? new List<Favorite>())
                        .Any(f => f.UserId == (int)context.Items["UserId"])
                    ));

            // Create → Entity
            CreateMap<CreateProductDto, Product>();

            CreateMap<NutritionFactDto, NutritionFact>();

            // Update → Entity
            CreateMap<UpdateProductDto, Product>();

            // Nutrition
            CreateMap<NutritionFact, NutritionFactDto>();
        }
    }
}