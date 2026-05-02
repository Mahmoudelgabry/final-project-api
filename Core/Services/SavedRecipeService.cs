using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SavedRecipeService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISavedRecipeService
    {


        public async Task ToggleSaveAsync(int userId, int recipeId)
        {
            var repo = _unitOfWork.SavedRecipeRepository;

            var existing = await repo.GetAsync(userId, recipeId);

            if (existing == null)
            {
                await repo.AddAsync(new SavedRecipe
                {
                    UserId = userId,
                    RecipeId = recipeId
                    
                });
            }
            else
            {
                repo.Delete(existing);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SavedRecipeDto>> GetUserSavedRecipesAsync(int userId)
        {
            var data = await _unitOfWork.SavedRecipeRepository
                .GetUserSavedRecipesAsync(userId);

            var result = _mapper.Map<IEnumerable<SavedRecipeDto>>(data);

            foreach (var item in result)
            {
                item.IsSaved = true; // 🔥 لأنه أصلاً saved
            }

            return result;
        }
    }
}
