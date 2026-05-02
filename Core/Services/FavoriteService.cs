using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class FavoriteService(IUnitOfWork _unitOfWork, IMapper _mapper) : IFavoriteService
    {


        public async Task ToggleSaveAsync(int userId, int productId)
        {
            var repo = _unitOfWork.FavoriteRepository;

            var existing = await repo.GetFavoriteAsync(userId, productId);

            if (existing == null)
            {
                await repo.AddAsync( new Favorite
                {
                    UserId = userId,
                    ProductId = productId
                });
            }
            else
            {
                repo.Delete(existing);
            }

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<FavoriteProductDto>> GetUserFavoritesAsync(int userId)
        {
            var favorites = await _unitOfWork
                .FavoriteRepository
                .GetUserFavoritesAsync(userId);

            var result = _mapper.Map<IEnumerable<FavoriteProductDto>>(favorites);

            foreach (var item in result)
            {
                item.IsSaved = true; 
            }

            return result;


        }
    }
}
