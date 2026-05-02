using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllAsync(int userId);

        Task<PostDto> GetByIdAsync(int id, int userId);
        

        Task CreateAsync(int userId, CreatePostDto dto);
        Task DeleteAsync(int userId, int postId, string role);
    }
}
