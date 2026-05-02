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
        public class FavoritesController(IServiceManager serviceManager) : ControllerBase
        {
            
        [HttpPost("{productId}/save")]
        public async Task<IActionResult> Toggle(int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.FavoriteService.ToggleSaveAsync(userId, productId);

            return Ok();
        }

        [HttpGet]
            public async Task<IActionResult> GetUserFavorites()
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var favorites = await serviceManager
                    .FavoriteService
                    .GetUserFavoritesAsync(userId);

                return Ok(favorites);
            }
        }
    }
