using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);

        Task<Comment?> GetByIdAsync(int id);

        Task AddAsync(Comment comment);

        void Delete(Comment comment);
    }
}
