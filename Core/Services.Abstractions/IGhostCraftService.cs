using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IGhostCraftService
    {
        Task CreateAsync(int userId, CreateGhostCraftDto dto);
        Task<IEnumerable<GhostCraftResultDto>> GetAllAsync();
        Task UpdateStatusAsync(int orderId, string status);
    }
}
