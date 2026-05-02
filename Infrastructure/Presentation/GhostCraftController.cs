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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await serviceManager.GhostCraftService.CreateAsync(userId, dto);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GhostCraftResultDto>>> GetAll()
        {
            var result = await serviceManager.GhostCraftService.GetAllAsync();
            return Ok(result);
        }

        // Admin: Update Status (Approved / Rejected)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateGhostCraftStatusDto dto)
        {
            await serviceManager.GhostCraftService.UpdateStatusAsync(id, dto.Status);

            return Ok();
        }
    }
}
