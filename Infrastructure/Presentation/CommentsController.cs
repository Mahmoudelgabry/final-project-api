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
    [Route("api/posts/{postId}/comments")]
    public class CommentsController(IServiceManager serviceManager) : ControllerBase
    {
        

        // 📥 Get Comments
        [HttpGet]
        public async Task<IActionResult> Get(int postId)
        {
            var result = await serviceManager.CommentService.GetByPostAsync(postId);
            return Ok(result);
        }

        // ➕ Add Comment
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(int postId, CreateCommentDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.CommentService.AddAsync(userId, postId, dto);

            return Ok();
        }

        // ❌ Delete Comment
        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int postId, int commentId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.CommentService.DeleteAsync(userId, commentId);

            return Ok();
        }
    }
}
