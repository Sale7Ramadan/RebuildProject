using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReportRepository : IBaseRepositories<Report>
    {
        Task<bool> IncrementLikesAsync(int reportId);

       
       // Task<IEnumerable<Report>> GetMostLikedReportsAsync(int count);
        Task<IEnumerable<Report>> GetReportsByCityAsync(int cityId);
    }
}
