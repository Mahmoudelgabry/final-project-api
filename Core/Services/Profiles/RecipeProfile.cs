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
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeListDto>()
                .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.IsSaved,
                    opt => opt.MapFrom((src, dest, _, context) =>
                    src.SavedRecipes.Any(s => s.UserId == (int)context.Items["UserId"])));

            CreateMap<Recipe, RecipeDetailsDto>()
                .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.IsSaved,
                    opt => opt.MapFrom((src, dest, _, context) =>
                    src.SavedRecipes.Any(s => s.UserId == (int)context.Items["UserId"])));

            // 🔥 هنا الحل الحقيقي
            CreateMap<CreateIngredientDto, Ingredient>()
                .ForMember(dest => dest.QuantityDescription,
                    opt => opt.MapFrom(src => src.QuantityDescription))
                .ForMember(dest => dest.RecipeId,
                    opt => opt.Ignore());

            CreateMap<UpdateRecipeDto, Recipe>();
            CreateMap<CreateRecipeDto, Recipe>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Instruction, InstructionDto>();
            CreateMap<CreateInstructionsDto, Instruction>();
        }
    }
}