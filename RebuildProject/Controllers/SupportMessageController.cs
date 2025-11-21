using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var role = User.FindFirst("Role")?.Value;

            var messages = await _messageService.GetMessagesForUserAsync(userId, role);
            return Ok(messages);
        }

        [HttpGet("ticket/{ticketId}")]
        [Authorize]
        public async Task<IActionResult> GetMessagesByTicket(int ticketId)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var role = User.FindFirst("Role")?.Value;

            var messages = await _messageService.GetMessagesByTicketIdAsync(ticketId, userId, role);
            return Ok(messages);
        }

      
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateSupportMessageDto dto)
        {
            var message = await _messageService.AddAsync(dto);
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
