using Domain.Models;
using Shared;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListDto>> GetAllAsync(int userId);
        Task<ProductDetailsDto?> GetByIdAsync(int id, int userId);

        // 🔥 هنا التعديل
        Task<Product> CreateAsync(CreateProductDto dto);

        Task UpdateAsync(int id, UpdateProductDto dto);
        Task DeleteAsync(int id);
    }
}