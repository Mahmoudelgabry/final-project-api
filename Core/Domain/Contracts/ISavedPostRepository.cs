using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISavedPostRepository :  IGenericRepository<SavedPost, int>
    {
        Task<SavedPost?> GetAsync(int userId, int postId);

        Task<IEnumerable<CommunityPost>> GetSavedPosts(int userId);
    }
}
