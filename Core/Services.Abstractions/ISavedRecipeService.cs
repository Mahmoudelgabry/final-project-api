using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ISavedRecipeService
    {
        Task ToggleSaveAsync(int userId, int recipeId);

        Task<IEnumerable<SavedRecipeDto>> GetUserSavedRecipesAsync(int userId);
    }
}
