using AutoMapper;
using Domain.Models;
using Shared;
using Shared.DTOs;

namespace Services.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>();
        }
    }
}