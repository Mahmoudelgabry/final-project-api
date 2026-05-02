using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.Profiles
{
    

    public class SavedRecipeProfile : Profile
    {
        public SavedRecipeProfile()
        {
            CreateMap<SavedRecipe, SavedRecipeDto>()
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Recipe.Title))

                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Recipe.ImageUrl))

                .ForMember(dest => dest.PrepTime,
                    opt => opt.MapFrom(src => src.Recipe.PrepTime))

                .ForMember(dest => dest.DifficultyLevel,
                    opt => opt.MapFrom(src => src.Recipe.DifficultyLevel))

                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Recipe.Category.Name));
        }
    }
}
