using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPostRepository
    {
        Task<IEnumerable<CommunityPost>> GetAllAsync();

        Task<CommunityPost?> GetByIdAsync(int id);

        Task AddAsync(CommunityPost post);
        void Delete(CommunityPost post);
    }
}