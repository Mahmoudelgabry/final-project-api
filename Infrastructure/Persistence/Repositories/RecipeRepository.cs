using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RecipeRepository
    : GenericRepository<Recipe, int>, IRecipeRepository
    {
        public RecipeRepository(StoreContext context)
            : base(context) { }

        public async Task<IEnumerable<Recipe>> GetAllWithCategoryAsync()
            => await _dbSet
                .Include(r => r.Category)
                .Include(r => r.SavedRecipes)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Recipe?> GetByIdWithDetailsAsync(int id)
            => await _dbSet
                .Include(r => r.Category)
                .Include(r => r.SavedRecipes)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .FirstOrDefaultAsync(r => r.Id == id);
    }
}
