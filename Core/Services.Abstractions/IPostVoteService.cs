using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPostVoteService
    {
        Task VoteAsync(int userId, int postId, int voteType);
    }
}
