using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Security.Claims;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketController : ControllerBase
    {
        private readonly IBaseService<SupportTicket, SupportTicketDto, CreateSupportTicketDto, UpdateSupportTicketDto> _ticketService;
        private readonly ITicketService _supportTicket;
        public SupportTicketController(
            IBaseService<SupportTicket, SupportTicketDto, CreateSupportTicketDto, UpdateSupportTicketDto> ticketService
            ,ITicketService ticketService1)
        {
            _ticketService = ticketService;
            _supportTicket = ticketService1;
        }

        [HttpGet("my-tickets")]
        [Authorize]
        public async Task<IActionResult> GetTicketsForCurrentUser()
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var roleClaim = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");

            var userId = int.Parse(userIdClaim.Value);
            var role = roleClaim?.Value;

            var tickets = await _supportTicket.GetTicketsForUserAsync(userId, role);
            return Ok(tickets);
        }



        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }


        [HttpPost]
        [Authorize]

        public async Task<IActionResult> Create([FromBody] CreateSupportTicketDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);



         

           
            var ticket = await _supportTicket.AddTicketAsync(dto,userId);

            return Ok(ticket);
        }


        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupportTicketDto dto)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Admin" && role !="SuperAdmin")
                return Forbid("Only admins can update tickets.");

            var updated = await _ticketService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }



        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid user ID in token.");

            var ticket = await _ticketService.GetByIdAsync(id);
            if (ticket == null) return NotFound();

            if ((role != "Admin" &&role != "SuperAdmin")&& ticket.UserId != userId )
                return Forbid();

            var deleted = await _ticketService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

    }
}
