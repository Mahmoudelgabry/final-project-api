using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shared.DTOs;

namespace Services
{
    public class CategoryService(IUnitOfWork _unitOfWork, IMapper _mapper) : ICategoryService
    {
        public async Task<IEnumerable<CategoryDto>> GetCategoriesByType(CategoryType type)
        {
            var categories = await _unitOfWork
                .CategoryRepository
                .GetCategoriesByTypeAsync(type);

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            // ✅ check duplicate
            var existingCategories = await _unitOfWork
                .CategoryRepository
                .GetCategoriesByTypeAsync(dto.Type);

            if (existingCategories.Any(c => c.Name.ToLower() == dto.Name.ToLower()))
                throw new Exception("Category already exists");

            // ✅ map
            var category = _mapper.Map<Category>(dto);

            // ✅ add using generic repo
            var repo = _unitOfWork.GetRepository<Category, int>();
            await repo.AddAsync(category);

            // ✅ save
            await _unitOfWork.SaveChangesAsync();

            // ✅ return
            return _mapper.Map<CategoryDto>(category);
        }
    }
}