using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

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
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var role = User.FindFirst("Role")?.Value;

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
            var ticket = await _ticketService.AddAsync(dto);
            return Ok(ticket);
        }


        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupportTicketDto dto)
        {
            var updated = await _ticketService.UpdateAsync(id, dto);
            var userId = int.Parse(User.FindFirst("UserId")?.Value);
            var role = User.FindFirst("Role")?.Value;

            if (role != "Admin" && dto.UserId != userId)
                return Forbid();
            if (!updated) return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ticketService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
