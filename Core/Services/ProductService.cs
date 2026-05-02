using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductListDto>> GetAllAsync(int userId)
        {
            var repo = await _unitOfWork.ProductRepository.GetAllWithCategoryAsync();

            var productsMap = _mapper.Map<IEnumerable<ProductListDto>>(repo, opt =>
            {
                opt.Items["UserId"] = userId;
            });

            return productsMap;
        }

        public async Task<ProductDetailsDto?> GetByIdAsync(int id, int userId)
        {
            var repo = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);

            if (repo == null) return null;

            var product = _mapper.Map<ProductDetailsDto>(repo, opt =>
            {
                opt.Items["UserId"] = userId;
            });

            return product;
        }

        // 🔥🔥🔥 التعديل هنا
        public async Task<Product> CreateAsync(CreateProductDto dto)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();

            var product = _mapper.Map<Product>(dto);

            product.NutritionFact = _mapper.Map<NutritionFact>(dto.Nutrition);

            await repo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product; // 🔥 مهم جدًا
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);

            if (product == null)
                throw new NotFoundException("Product not found");

            _mapper.Map(dto, product);

            if (product.NutritionFact != null)
            {
                _mapper.Map(dto.Nutrition, product.NutritionFact);
            }
            else
            {
                product.NutritionFact = _mapper.Map<NutritionFact>(dto.Nutrition);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetAsync(id);

            if (product == null)
                throw new NotFoundException("Product not found");

            try
            {
                repo.Delete(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new BadRequestException("Cannot delete product because it is used in other data");
            }
        }
    }
}