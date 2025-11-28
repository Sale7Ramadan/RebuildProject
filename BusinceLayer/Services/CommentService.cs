using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class CommentService
    : BaseService<Comment, CommentDto, CreateCommentDto, UpdateCommentDto>, ICommentService
    {
        private readonly IBaseRepositories<Comment> _repository;
        private readonly IBaseRepositories<Report> _reportRepository;

        public CommentService(
            IBaseRepositories<Comment> repository,
            IBaseRepositories<Report> reportRepository,
            IMapper mapper
        )
            : base(repository, mapper)
        {
            _repository = repository;
            _reportRepository = reportRepository;
        }

       
        public async Task<CommentDto> AddCommentAsync(CreateCommentDto dto, int userId)
        {
            // تأكد أن البلاغ موجود
            var report = await _reportRepository.GetByIdAsync(dto.ReportId);
            if (report == null)
                return null;

            var comment = new Comment
            {
                CommentText = dto.CommentText,
                ReportId = dto.ReportId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(comment);

            return _mapper.Map<CommentDto>(comment);
        }

       
       

       
        public async Task<IEnumerable<CommentDto>> GetUserCommentsAsync(int userId)
        {
            var comments = await _repository.GetAllWithIncludeAsync(c => c.UserId == userId);

            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }
        public async Task<IEnumerable<CommentDto>> GetCommentsByReportAsync(int reportId)
        {
            var comments = await _repository
                .GetAllWithIncludeAsync(c => c.User, c => c.Report);

            var filtered = comments.Where(c => c.ReportId == reportId);

            return _mapper.Map<IEnumerable<CommentDto>>(filtered);
        }


    }
}
