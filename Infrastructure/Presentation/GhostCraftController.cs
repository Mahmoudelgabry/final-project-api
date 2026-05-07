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
    public class GhostCraftController(IServiceManager serviceManager) : ControllerBase
    {

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGhostCraftDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await serviceManager.GhostCraftService.CreateAsync(userId, dto);

            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GhostCraftResultDto>>> GetAll()
        {
            var result = await serviceManager.GhostCraftService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateGhostCraftDto dto)
          {
  
            var result = await serviceManager
                .GhostCraftService
                .UpdateAsync(id, dto);

            return Ok(result);
          }
    }
}
