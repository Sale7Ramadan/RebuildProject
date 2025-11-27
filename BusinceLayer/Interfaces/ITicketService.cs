using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface ITicketService : IBaseService<SupportTicket, SupportTicketDto, CreateSupportTicketDto, UpdateSupportTicketDto>
    {
        Task<IEnumerable<SupportTicketDto>> GetTicketsForUserAsync(int userId, string role);
        Task<SupportTicketDto> AddTicketAsync(CreateSupportTicketDto dto, int userId);
    }
}
