using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController(IServiceManager serviceManager) : ControllerBase
    {

        // GET: api/recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeListDto>>> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recipes = await serviceManager.RecipeService.GetAllRecipesAsync(userId);
            return Ok(recipes);
        }


        // GET: api/recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailsDto>> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var recipe = await serviceManager.RecipeService.GetRecipeByIdAsync(id, userId);

            

            return Ok(recipe);
        }


        // CREATE Recipe (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeDto dto)
        {
            await serviceManager.RecipeService.CreateRecipeAsync(dto);
            return Ok();
        }


        // UPDATE Recipe (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateRecipeDto dto)
        {
            await serviceManager.RecipeService.UpdateRecipeAsync(id, dto);
            return Ok();
        }


        // DELETE Recipe (Admin)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await serviceManager.RecipeService.DeleteRecipeAsync(id);
            return Ok();
        }
    }
}