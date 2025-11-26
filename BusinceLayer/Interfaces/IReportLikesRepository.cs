using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IReportLikesService
    {
        Task<bool> AddLikeAsync(int userId, int reportId);
        Task<bool> RemoveLikeAsync(int userId, int reportId);
        Task<int> GetLikesCountAsync(int reportId);
        Task<IEnumerable<ReportsLikeDto>> GetUsersWhoLikedAsync(int reportId);
    }


}
