using Microsoft.AspNetCore.Mvc;
using SOA_CA2_E_Commerce.DTO;
using SOA_CA2_E_Commerce.Services;

namespace SOA_CA2_E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null) return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _categoryService.AddCategoryAsync(categoryDto);
            if (!success) return StatusCode(500, "Unable to add category.");
            return Ok("Category added successfully.");
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDTO categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _categoryService.UpdateCategoryAsync(categoryId, categoryDto);
            if (!success) return NotFound("Category not found.");
            return Ok("Category updated successfully.");
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var success = await _categoryService.DeleteCategoryAsync(categoryId);
            if (!success) return NotFound("Category not found.");
            return Ok("Category deleted successfully.");
        }
    }
}
