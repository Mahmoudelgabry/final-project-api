using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SavedPostRepository :GenericRepository<SavedPost, int>, ISavedPostRepository
    {
        public SavedPostRepository(StoreContext context) : base(context) { }

        public async Task<SavedPost?> GetAsync(int userId, int postId)
            => await _dbSet
                .FirstOrDefaultAsync(s => s.UserId == userId && s.PostId == postId);


        public async Task<IEnumerable<CommunityPost>> GetSavedPosts(int userId)
            => await _dbSet
         .Include(s => s.Post)
            .ThenInclude(p => p.User)
        .Include(s => s.Post)
            .ThenInclude(p => p.PostTags).ThenInclude(pt => pt.Tag)
        .Include(s => s.Post)
            .ThenInclude(p => p.Votes)
        .Include(s => s.Post)
            .ThenInclude(p => p.SavedPosts)
        .Include(s => s.Post)
            .ThenInclude(p => p.Comments)
        .Where(s => s.UserId == userId)
        .Select(s => s.Post)
        .ToListAsync();
    }
}
