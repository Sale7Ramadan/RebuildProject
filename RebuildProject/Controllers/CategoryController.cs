using BusinceLayer.Services;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IBaseService<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto> _categoryService;

        public CategoryController(IBaseService<Category, CategoryDto, CreateCategoryDto, UpdateCategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound($"Category with ID {id} not found.");

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _categoryService.AddAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.UpdateAsync(id, updateCategoryDto);
            if (!result)
                return NotFound($"Category with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Category with ID {id} not found.");

            return NoContent();
        }
    }
}
