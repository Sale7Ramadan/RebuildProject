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

        public MessageService(IBaseRepositories<SupportMessage> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

      
        public async Task<IEnumerable<SupportMessageDto>> GetMessagesForUserAsync(int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin")
            {
                messages = await _repository.GetAllAsync();
            }
            else
            {
                messages = await _repository.GetAllWithIncludeAsync(m => m.Ticket.UserId == userId);
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }

        
        public async Task<IEnumerable<SupportMessageDto>> GetMessagesByTicketIdAsync(int ticketId, int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin")
            {
                messages = await _repository.GetAllWithIncludeAsync(m => m.TicketId == ticketId);
            }
            else
            {
                messages = await _repository.GetAllWithIncludeAsync(m => m.TicketId == ticketId && m.Ticket.UserId == userId);
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }
    }
}
