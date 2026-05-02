using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shareds.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PostVoteService(IPostVoteRepository repo, IUnitOfWork unitOfWork) : IPostVoteService
    {
        

        public async Task VoteAsync(int userId, int postId, int voteType)
        {
            if (voteType != 1 && voteType != -1)
                throw new BadRequestException("Vote must be 1 or -1");

            var existing = await repo.GetAsync(userId, postId);

            if (existing == null)
            {
                await repo.AddAsync(new PostVote
                {
                    UserId = userId,
                    PostId = postId,
                    VoteType = voteType
                });
            }
            else
            {
                if (existing.VoteType == voteType)
                {
                    repo.Delete(existing); // toggle
                }
                else
                {
                    existing.VoteType = voteType; // switch
                }
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}
