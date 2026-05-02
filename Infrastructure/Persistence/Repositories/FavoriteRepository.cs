using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class FavoriteRepository
    : GenericRepository<Favorite, int>, IFavoriteRepository
    {
        public FavoriteRepository(StoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId)
            => await _dbSet
                .Include(f => f.Product).ThenInclude(f => f.Category)
                .Where(f => f.UserId == userId)
                .ToListAsync();

        public async Task<Favorite?> GetFavoriteAsync(int userId, int productId)
            => await _dbSet
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId &&
                    f.ProductId == productId);
    }
}
