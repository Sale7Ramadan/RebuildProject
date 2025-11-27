using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IMessageService :
        IBaseService<SupportMessage, SupportMessageDto, CreateSupportMessageDto, UpdateSupportMessageDto>
    {
        
        Task<IEnumerable<SupportMessageDto>> GetMessagesForUserAsync(int userId, string role);

      
        Task<IEnumerable<SupportMessageDto>> GetMessagesByTicketIdAsync(int ticketId, int userId, string role);

        Task<SupportMessageDto> AddMessageAsync(CreateSupportMessageDto dto, int senderId, bool isAdmin);

    }
}
