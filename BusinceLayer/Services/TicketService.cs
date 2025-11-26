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

            if (role == "Admin")
                tickets = await _repository.GetAllAsync();
            else
                tickets = await _repository.GetAllWithIncludeAsync(t => t.UserId == userId);

            return _mapper.Map<IEnumerable<SupportTicketDto>>(tickets);
        }
    }
}
