using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class ReportLikesService : IReportLikesService
    {
        private readonly IReportLikesRepository _repo;
        private readonly IMapper _mapper;

        public ReportLikesService(IReportLikesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> AddLikeAsync(int userId, int reportId)
        {
            if (await _repo.HasUserLikedAsync(userId, reportId))
                return false;

            var like = new ReportsLikes
            {
                UserId = userId,
                ReportId = reportId,
                
            };

            await _repo.AddAsync(like);
            return true;
        }

        public async Task<bool> RemoveLikeAsync(int userId, int reportId)
        {
            var like = await _repo.GetUserLikeAsync(userId, reportId);
            if (like == null)
                return false;

            await _repo.DeleteAsync(like.ReportLikeId);
            return true;
        }

        public async Task<int> GetLikesCountAsync(int reportId)
            => await _repo.GetLikesCountAsync(reportId);

        public async Task<IEnumerable<ReportsLikeDto>> GetUsersWhoLikedAsync(int reportId)
        {
            var likes = await _repo.GetLikesWithUsersAsync(reportId);
            return _mapper.Map<IEnumerable<ReportsLikeDto>>(likes);
        }
    }

}
