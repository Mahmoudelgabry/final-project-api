using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductRepository
    : GenericRepository<Product, int>, IProductRepository
    {
        public ProductRepository(StoreContext context)
            : base(context) { }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
            => await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Favorites) 
                .AsNoTracking()
                .ToListAsync();

        public async Task<Product?> GetByIdWithDetailsAsync(int id)
            => await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Favorites)
                .Include(p => p.NutritionFact)
                .FirstOrDefaultAsync(p => p.Id == id);
    }   
}
