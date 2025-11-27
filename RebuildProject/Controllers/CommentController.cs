using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IBaseService<Comment, CommentDto, CreateCommentDto, UpdateCommentDto> _commentService;
        private readonly ICommentService commentService1;

        public CommentController(IBaseService<Comment, CommentDto, CreateCommentDto, UpdateCommentDto> commentService
            , ICommentService commentService1)
        {
            _commentService = commentService;
            this.commentService1 = commentService1;

        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAllWithIncludeAsync(u => u.User);
            return Ok(comments);
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetAllWithIncludeAsync(u => u.User);
            var commentResult = comment.FirstOrDefault(c => c.CommentId == id);
            if (commentResult == null)
                return NotFound($"Comment with ID {id} not found.");

            return Ok(commentResult);
        }

        // POST: api/Comment
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await commentService1.AddCommentAsync(createCommentDto, userId);

            if (result == null)
                return NotFound("Report not found.");

            return Ok(result);
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
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int editorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            // جلب التعليق
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found.");

            // تحقق إذا صاحب التعليق أو Admin/SuperAdmin
            if (comment.UserId != editorId && role != "Admin" && role != "SuperAdmin")
                return Forbid();

            // الحذف
            var deleted = await _commentService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"Comment with ID {id} not found.");

            return NoContent();
        }
    }
}
