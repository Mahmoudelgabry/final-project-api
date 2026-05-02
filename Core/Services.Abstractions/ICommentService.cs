using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetByPostAsync(int postId);

        Task AddAsync(int userId, int postId, CreateCommentDto dto);

        Task DeleteAsync(int userId, int commentId);
    }
}
