using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SavedRecipeRepository
    : GenericRepository<SavedRecipe, int>, ISavedRecipeRepository
    {
        public SavedRecipeRepository(StoreContext context) : base(context) { }

        public async Task<IEnumerable<SavedRecipe>> GetUserSavedRecipesAsync(int userId)
            => await _dbSet
                .Include(x => x.Recipe).ThenInclude(r => r.Category)
                .Where(x => x.UserId == userId)
                .ToListAsync();

        public async Task<SavedRecipe?> GetAsync(int userId, int recipeId)
            => await _dbSet
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId && x.RecipeId == recipeId);
    }
}
