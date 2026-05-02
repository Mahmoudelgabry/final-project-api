using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _storeContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
            _dbSet = _storeContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges)
            => trackChanges
                ? await _dbSet.ToListAsync()
                : await _dbSet.AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
            => await _dbSet.FindAsync(id);

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public void Update(TEntity entity) => _dbSet.Update(entity);
    }
}