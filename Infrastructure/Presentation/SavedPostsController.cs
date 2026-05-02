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
    [Route("api/posts")]
    [Authorize]
    public class SavedPostsController(IServiceManager serviceManager) : ControllerBase
    {
        // 🔥 Save / Unsave
        [HttpPost("{postId}/save")]
        public async Task<IActionResult> Toggle(int postId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.SavedPostService.ToggleSaveAsync(userId, postId);

            return NoContent();
        }

        // 🔥 Get Saved Posts (صح)
        [HttpGet("saved")]
        public async Task<IActionResult> GetSaved()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await serviceManager.SavedPostService.GetSavedPostsAsync(userId));
        }
    }
}

