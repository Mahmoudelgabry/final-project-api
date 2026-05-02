using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;


namespace Services
{
    public class RecipeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IRecipeService
    {
        
        public async Task<IEnumerable<RecipeListDto>> GetAllRecipesAsync(int userId)
        {
            var recipes = await _unitOfWork.RecipeRepository
             .GetAllWithCategoryAsync();

            return  _mapper.Map<IEnumerable<RecipeListDto>>(recipes, opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }

        public async Task<RecipeDetailsDto?> GetRecipeByIdAsync(int id, int userId)
        {
            var recipe = await _unitOfWork.RecipeRepository
               .GetByIdWithDetailsAsync(id);

            if (recipe == null)
                throw new NotFoundException("recipe not found");

            return _mapper.Map<RecipeDetailsDto>(recipe,opt =>
            {
                opt.Items["UserId"] = userId;
            });
        }

        public async Task CreateRecipeAsync(CreateRecipeDto dto)
        {
            var repo = _unitOfWork.GetRepository<Recipe, int>();

            var recipe = _mapper.Map<Recipe>(dto);

            recipe.Ingredients = _mapper.Map<List<Ingredient>>(dto.Ingredients);

            await repo.AddAsync(recipe);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(int id, UpdateRecipeDto dto)
        {
            var recipe = await _unitOfWork.RecipeRepository
                .GetByIdWithDetailsAsync(id);

            if (recipe == null)
                throw new NotFoundException("Recipe not found");

            _mapper.Map(dto, recipe);

            // تحديث Ingredients
            recipe.Ingredients.Clear();

            foreach (var ingredient in dto.Ingredients)
            {
                recipe.Ingredients.Add(new Ingredient
                {
                    QuantityDescription = ingredient.QuantityDescription
                });
            }

            // تحديث Instructions
            recipe.Instructions.Clear();

            foreach (var step in dto.Instructions)
            {
                recipe.Instructions.Add(new Instruction
                {
                    Step = step.Step
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Recipe, int>();

            var recipe = await repo.GetAsync(id);

            if (recipe == null)
                throw new NotFoundException("Recipe not found");

            repo.Delete(recipe);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
