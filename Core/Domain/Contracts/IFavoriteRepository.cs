using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IFavoriteRepository : IGenericRepository<Favorite, int>
    {
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId);

        Task<Favorite?> GetFavoriteAsync(int userId, int productId);
    }
}
