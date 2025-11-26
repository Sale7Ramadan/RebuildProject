using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReportLikesRepository : IBaseRepositories<ReportsLikes>
    {
        Task<bool> HasUserLikedAsync(int userId, int reportId);
        Task<ReportsLikes> GetUserLikeAsync(int userId, int reportId);
        Task<int> GetLikesCountAsync(int reportId);
        Task<IEnumerable<ReportsLikes>> GetLikesWithUsersAsync(int reportId);
    }

}
