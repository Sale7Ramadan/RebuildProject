using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class TicketService : BaseService<SupportTicket, SupportTicketDto, CreateSupportTicketDto, UpdateSupportTicketDto>, ITicketService
    {
        private readonly IBaseRepositories<SupportTicket> _repository;
        private readonly IMapper _mapper;

        public TicketService(IBaseRepositories<SupportTicket> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupportTicketDto>> GetTicketsForUserAsync(int userId, string role)
        {
            IEnumerable<SupportTicket> tickets;

            // اجلب التذاكر + Include
            var allTickets = await _repository.GetAllWithIncludeAsync(
                t => t.User,
                t => t.Messages
            );

            if (role == "Admin")
                tickets = allTickets;
            else
                tickets = allTickets.Where(t => t.UserId == userId).ToList();

            return _mapper.Map<IEnumerable<SupportTicketDto>>(tickets);
        }
        public async Task<SupportTicketDto> AddTicketAsync(CreateSupportTicketDto dto, int userId)
        {
          
            var ticket = _mapper.Map<SupportTicket>(dto);

            
            ticket.UserId = userId;
            ticket.CreatedAt = DateTime.UtcNow;

         
            await _repository.AddAsync(ticket);

        
            return _mapper.Map<SupportTicketDto>(ticket);
        }



    }
}
