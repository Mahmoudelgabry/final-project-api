using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.DTOs;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("{type}")]
        public async Task<IActionResult> GetCategoriesByType(CategoryType type)
        {
            var result = await serviceManager.CategoryService.GetCategoriesByType(type);
            return Ok(result);
        }

        // 🔥 NEW: Add Category (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var result = await serviceManager.CategoryService.CreateCategoryAsync(dto);
            return Ok(result);
        }
    }
}