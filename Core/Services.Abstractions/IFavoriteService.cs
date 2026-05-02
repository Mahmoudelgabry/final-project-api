using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IFavoriteService
    {
        Task ToggleSaveAsync(int userId, int productId);

        

        Task<IEnumerable<FavoriteProductDto>> GetUserFavoritesAsync(int userId);
    }
}
