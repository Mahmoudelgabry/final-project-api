using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPostVoteRepository
    {
        Task<PostVote?> GetAsync(int userId, int postId);

        Task AddAsync(PostVote vote);

        void Delete(PostVote vote);
    }
}
