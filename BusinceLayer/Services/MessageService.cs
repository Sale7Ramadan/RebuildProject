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
    public class MessageService : BaseService<SupportMessage, SupportMessageDto, CreateSupportMessageDto, UpdateSupportMessageDto>, IMessageService
    {
        private readonly IBaseRepositories<SupportMessage> _repository;
        private readonly IMapper _mapper;
        private readonly IBaseRepositories<SupportTicket> _ticketRepository;

        public MessageService(IBaseRepositories<SupportMessage> repository,
            IBaseRepositories<SupportTicket> ticketRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

      
        public async Task<IEnumerable<SupportMessageDto>> GetMessagesForUserAsync(int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin")
            {
                messages = await _repository.GetAllWithIncludeAsync(m=>m.Ticket);
            }
            else
            {
                var all = await _repository.GetAllWithIncludeAsync(m => m.Ticket);
                messages = all.Where(m => m.Ticket.UserId == userId);
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }


        public async Task<IEnumerable<SupportMessageDto>> GetMessagesByTicketIdAsync(int ticketId, int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin")
            {
              
                messages = await _repository.GetAllWithIncludeAsync(m => m.Ticket);
                messages = messages.Where(m => m.TicketId == ticketId);
            }
            else
            {
            
                var all = await _repository.GetAllWithIncludeAsync(m => m.Ticket);
           
                messages = all.Where(m => m.TicketId == ticketId && m.Ticket.UserId == userId);
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }

        public async Task<SupportMessageDto> AddMessageAsync(CreateSupportMessageDto dto, int senderId, bool isAdmin)
        {
           
            var ticket = await _ticketRepository.GetByIdAsync(dto.TicketId);
            if (ticket == null)
                throw new Exception("Ticket not found");

           
            if (!isAdmin && ticket.UserId != senderId)
                throw new Exception("You are not allowed to send a message to this ticket");

           
            var message = new SupportMessage
            {
                TicketId = dto.TicketId,
                Message = dto.Message,
                UserId = senderId,
                IsFromAdmin = isAdmin,
                CreatedAt = DateTime.UtcNow
            };

          
            await _repository.AddAsync(message);

           
            return _mapper.Map<SupportMessageDto>(message);
        }


    }
}
