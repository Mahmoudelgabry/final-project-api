using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ISavedPostService
    {
        Task ToggleSaveAsync(int userId, int postId);
        

        Task<IEnumerable<PostDto>> GetSavedPostsAsync(int userId);
    }
}
