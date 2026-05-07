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
    [Route("api/[controller]")]
    public class PostsController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await serviceManager.PostService.GetAllAsync(userId));
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await serviceManager.PostService.GetByIdAsync(id,userId));
        }
            

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.PostService.CreateAsync(userId, dto);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            await serviceManager.PostService.DeleteAsync(userId,  id, role);

            return Ok();
        }
    }
}
