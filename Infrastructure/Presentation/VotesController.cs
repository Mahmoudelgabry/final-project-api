using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/posts/{postId}/vote")]
    [Authorize]
    public class VotesController(IServiceManager serviceManager) : ControllerBase
    {
        

        [HttpPost]
        public async Task<IActionResult> Vote(int postId, VoteDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.PostVoteService.VoteAsync(userId, postId, dto.VoteType);

            return NoContent();
        }
    }
}
