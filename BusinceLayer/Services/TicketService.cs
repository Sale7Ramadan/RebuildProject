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
        private readonly IBaseRepositories<User> _userRepository;
        private readonly IBaseRepositories<City> _cityRepository;

        public TicketService(IBaseRepositories<SupportTicket> repository, IMapper mapper,IBaseRepositories<User> user,IBaseRepositories<City> city) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = user;
            _cityRepository = city;
        }

        public async Task<IEnumerable<SupportTicketDto>> GetTicketsForUserAsync(int userId, string role)
        {
            IEnumerable<SupportTicket> tickets;

            // اجلب التذاكر + Include
            var allTickets = await _repository.GetAllWithIncludeAsync(
                t => t.User,
                t => t.Messages,
                t => t.City
            );

            if (role == "Admin")
                tickets = allTickets;
            else if (role == "SuperAdmin")
                tickets = allTickets;
            else
                tickets = allTickets.Where(t => t.UserId == userId).ToList();

            return _mapper.Map<IEnumerable<SupportTicketDto>>(tickets);
        }
        public async Task<SupportTicketDto> AddTicketAsync(CreateSupportTicketDto dto, int userId)
        {
            // اجلب المستخدم
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            // اجلب المدينة
            var city = await _cityRepository.GetByIdAsync(dto.CityId);
            if (city == null)
                throw new Exception("City not found.");

            // انشاء التذكرة
            var ticket = _mapper.Map<SupportTicket>(dto);
            ticket.UserId = userId;
            ticket.CreatedAt = DateTime.UtcNow;

            // تعديل العنوان تلقائياً
            ticket.Title = $"{user.FirstName} {user.LastName} - {city.CityName}";

            // حفظ
            await _repository.AddAsync(ticket);

            // 🟢 اجلب التذكرة من جديد مع Include
            var fullTicket = (await _repository.GetAllWithIncludeAsync(
                t => t.User,
                t => t.City
            )).FirstOrDefault(t => t.Id == ticket.Id);

            if (fullTicket == null)
                throw new Exception("Ticket not found after creation.");

            // رجاع DTO كامل
            return _mapper.Map<SupportTicketDto>(fullTicket);
        }


    }
}
