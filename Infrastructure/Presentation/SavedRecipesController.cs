using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SavedRecipesController(IServiceManager serviceManager) : ControllerBase
    {


        [HttpPost("{recipeId}/save")]
        public async Task<IActionResult> Toggle(int recipeId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.SavedRecipeService.ToggleSaveAsync(userId, recipeId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var data = await serviceManager.SavedRecipeService
                .GetUserSavedRecipesAsync(userId);

            return Ok(data);
        }
    }
}
