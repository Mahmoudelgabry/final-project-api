using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeListDto>> GetAllRecipesAsync(int userId);

        Task<RecipeDetailsDto?> GetRecipeByIdAsync(int id, int userId);

        Task CreateRecipeAsync(CreateRecipeDto dto);

        Task UpdateRecipeAsync(int id, UpdateRecipeDto dto);

        Task DeleteRecipeAsync(int id);
    }
}
