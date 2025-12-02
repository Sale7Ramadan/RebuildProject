using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class MessageService : BaseService<SupportMessage, SupportMessageDto, CreateSupportMessageDto, UpdateSupportMessageDto>, IMessageService
    {
        private readonly IBaseRepositories<SupportMessage> _repository;
        private readonly IMapper _mapper;
        private readonly IBaseRepositories<SupportTicket> _ticketRepository;

        public MessageService(
            IBaseRepositories<SupportMessage> repository,
            IBaseRepositories<SupportTicket> ticketRepository,
            IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        // جميع الرسائل للمستخدم أو للـ Admin/SuperAdmin
        public async Task<IEnumerable<SupportMessageDto>> GetMessagesForUserAsync(int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin" || role == "SuperAdmin")
            {
                // جلب كل الرسائل
                messages = await _repository.GetAllWithIncludeAndFilterAsync(
                    null, // لا فلترة
                    m => m.Ticket
                );
            }
            else
            {
                // المستخدم العادي يرى فقط رسائله
                messages = await _repository.GetAllWithIncludeAndFilterAsync(
                    m => m.Ticket.UserId == userId,
                    m => m.Ticket
                );
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }

        // الرسائل الخاصة بتكت محدد
        public async Task<IEnumerable<SupportMessageDto>> GetMessagesByTicketIdAsync(int ticketId, int userId, string role)
        {
            IEnumerable<SupportMessage> messages;

            if (role == "Admin" || role == "SuperAdmin")
            {
                messages = await _repository.GetAllWithIncludeAndFilterAsync(
                    m => m.TicketId == ticketId,
                    m => m.Ticket
                );
            }
            else
            {
                messages = await _repository.GetAllWithIncludeAndFilterAsync(
                    m => m.TicketId == ticketId && m.Ticket.UserId == userId,
                    m => m.Ticket
                );
            }

            return _mapper.Map<IEnumerable<SupportMessageDto>>(messages);
        }

        // إضافة رسالة جديدة
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

        // تحديث رسالة
        public async Task<bool> UpdateAsync(int id, UpdateSupportMessageDto dto)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null) return false;

            message.Message = dto.Message;
            await _repository.UpdateAsync(message);
            return true;
        }

        // حذف رسالة
        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _repository.GetByIdAsync(id);
            if (message == null) return false;

            await _repository.DeleteAsync(message.Id);
            return true;
        }
    }
}
