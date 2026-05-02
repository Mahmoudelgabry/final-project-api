using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly StoreContext _context;

        public CommentRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
            => await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<Comment?> GetByIdAsync(int id)
            => await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Comment comment)
            => await _context.Comments.AddAsync(comment);

        public void Delete(Comment comment)
            => _context.Comments.Remove(comment);
    }
}
