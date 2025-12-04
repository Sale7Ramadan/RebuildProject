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
    public class SupportMessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public SupportMessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("my-messages")]
        [Authorize]
        public async Task<IActionResult> GetMessagesForCurrentUser()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid or missing user ID in token.");

            var messages = await _messageService.GetMessagesForUserAsync(userId, role);
            return Ok(messages);
        }

        [HttpGet("ticket/{ticketId}")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByTicket(int ticketId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid or missing user ID in token.");

            var messages = await _messageService.GetMessagesByTicketIdAsync(ticketId, userId, role);
            return Ok(messages);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSupportMessageDto dto)
        {
            var senderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = role == "Admin" || role == "SuperAdmin";


            var message = await _messageService.AddMessageAsync(dto, senderId, isAdmin);
            return Ok(message);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupportMessageDto dto)
        {
            var updated = await _messageService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _messageService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
