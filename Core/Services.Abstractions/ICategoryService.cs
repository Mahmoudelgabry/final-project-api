using Shared;
using Shared.DTOs;
using Domain.Models;

namespace Services.Abstractions
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesByType(CategoryType type);

        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto);
    }
}