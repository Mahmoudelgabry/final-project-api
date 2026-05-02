using Domain.Contracts;

using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{

    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        

        public CategoryRepository(StoreContext context)
        : base(context) { }

        public async Task<IEnumerable<Category>> GetCategoriesByTypeAsync(CategoryType type)
        {
            return await _dbSet
                .Where(c => c.Type == type)
                .ToListAsync();
        }
    }
}
