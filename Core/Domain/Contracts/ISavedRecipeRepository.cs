using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISavedRecipeRepository : IGenericRepository<SavedRecipe, int>
    {
        Task<IEnumerable<SavedRecipe>> GetUserSavedRecipesAsync(int userId);

        Task<SavedRecipe?> GetAsync(int userId, int recipeId);
    }
}
