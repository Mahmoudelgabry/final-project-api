using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IRecipeRepository : IGenericRepository<Recipe, int>
    {
        Task<IEnumerable<Recipe>> GetAllWithCategoryAsync();

        Task<Recipe?> GetByIdWithDetailsAsync(int id);
    }
}
