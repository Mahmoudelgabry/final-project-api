using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly StoreContext _context;

        public PostRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityPost>> GetAllAsync()
            => await _context.CommunityPosts
                .Include(p => p.User)
                .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Votes)
                .Include(p => p.Comments)
                .Include(p => p.SavedPosts)
                .ToListAsync();

        public async Task<CommunityPost?> GetByIdAsync(int id)
            => await _context.CommunityPosts
                .Include(p => p.User)
                .Include(p => p.Votes)
                .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Comments)
                .Include(p => p.SavedPosts)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(CommunityPost post)
            => await _context.CommunityPosts.AddAsync(post);
        public void Delete(CommunityPost post)
            => _context.CommunityPosts.Remove(post);
    }
}
