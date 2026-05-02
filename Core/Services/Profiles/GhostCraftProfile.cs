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

                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(_ => "Pending"));
            CreateMap<GhostCraftOrder, GhostCraftResultDto>()
                .ForMember(dest => dest.Allergies,
                    opt => opt.MapFrom(src => src.Allergies.Split(',', StringSplitOptions.RemoveEmptyEntries)))

               .ForMember(dest => dest.DietaryPreferences,
                    opt => opt.MapFrom(src => src.DietaryPreferences.Split(',', StringSplitOptions.RemoveEmptyEntries)));
        }
    }
}
