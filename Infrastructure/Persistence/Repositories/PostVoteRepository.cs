using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class PostVoteRepository : IPostVoteRepository
    {
        private readonly StoreContext _context;

        public PostVoteRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<PostVote?> GetAsync(int userId, int postId)
            => await _context.PostVotes
                .FirstOrDefaultAsync(v => v.UserId == userId && v.PostId == postId);

        public async Task AddAsync(PostVote vote)
            => await _context.PostVotes.AddAsync(vote);

        public void Delete(PostVote vote)
            => _context.PostVotes.Remove(vote);
    }
}
