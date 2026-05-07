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
    public class GhostCraftProfile : Profile
    {
        public GhostCraftProfile()
        {
            CreateMap<CreateGhostCraftDto, GhostCraftOrder>()
                .ForMember(dest => dest.Allergies,
                    opt => opt.MapFrom(src => string.Join(",", src.Allergies)))

                .ForMember(dest => dest.DietaryPreferences,
                    opt => opt.MapFrom(src => string.Join(",", src.DietaryPreferences)))

                

                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => GetPrice(src.PortionSize)));
            CreateMap<UpdateGhostCraftDto, GhostCraftOrder>()
               .ForMember(dest => dest.Allergies,
                   opt => opt.MapFrom(src => string.Join(",", src.Allergies)))

              .ForMember(dest => dest.DietaryPreferences,
                  opt => opt.MapFrom(src => string.Join(",", src.DietaryPreferences)))

              .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => GetPrice(src.PortionSize)));
            CreateMap<GhostCraftOrder, GhostCraftResultDto>()
                .ForMember(dest => dest.Allergies,
                    opt => opt.MapFrom(src =>
                        src.Allergies.Split(',', StringSplitOptions.RemoveEmptyEntries)))

                .ForMember(dest => dest.DietaryPreferences,
                    opt => opt.MapFrom(src =>
                        src.DietaryPreferences.Split(',', StringSplitOptions.RemoveEmptyEntries)));
        }

        private static decimal GetPrice(string portionSize)
        {
            return portionSize switch
            {
                "Small" => 12.99m,
                "Medium" => 18.99m,
                "Large" => 24.99m,
                "Family" => 32.99m,
                _ => throw new Exception("Invalid portion size")
            };
        }
    }
}
