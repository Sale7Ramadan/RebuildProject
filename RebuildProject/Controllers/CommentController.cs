using BusinceLayer.Services;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IBaseService<Comment, CommentDto, CreateCommentDto, UpdateCommentDto> _commentService;

        public CommentController(IBaseService<Comment, CommentDto, CreateCommentDto, UpdateCommentDto> commentService)
        {
            _commentService = commentService;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            return Ok(comment);
        }

        // POST: api/Comment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdComment = await _commentService.AddAsync(createCommentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdComment.CommentId }, createdComment);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentService.UpdateAsync(id, updateCommentDto);
            if (!result)
                return NotFound($"Comment with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _commentService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Comment with ID {id} not found.");

            return NoContent();
        }
    }
}
