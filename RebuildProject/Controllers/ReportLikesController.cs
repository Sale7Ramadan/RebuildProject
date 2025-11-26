using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportLikesController : ControllerBase
    {


        private readonly IReportLikesService _service;

        public ReportLikesController(IReportLikesService service)
        {
            _service = service;
        }

        // POST: api/ReportLikes/{reportId}/like
        [HttpPost("{reportId}/like")]
        public async Task<IActionResult> AddLike(int reportId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.AddLikeAsync(userId, reportId);

            if (!result)
                return BadRequest("You have already liked this report.");

            return Ok("Like added successfully.");
        }

        // DELETE: api/ReportLikes/{reportId}/like
        [HttpDelete("{reportId}/like")]
        public async Task<IActionResult> RemoveLike(int reportId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _service.RemoveLikeAsync(userId, reportId);

            if (!result)
                return NotFound("Like not found.");

            return Ok("Like removed successfully.");
        }

        // GET: api/ReportLikes/{reportId}/count
        [AllowAnonymous]
        [HttpGet("{reportId}/count")]
        public async Task<IActionResult> GetLikesCount(int reportId)
        {
            var count = await _service.GetLikesCountAsync(reportId);
            return Ok(count);
        }

        // GET: api/ReportLikes/{reportId}/users
        [AllowAnonymous]
        [HttpGet("{reportId}/users")]
        public async Task<IActionResult> GetUsersWhoLiked(int reportId)
        {
            var users = await _service.GetUsersWhoLikedAsync(reportId);
            return Ok(users);
        }


    }
}
