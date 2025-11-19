using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositries
{
    public class ReportRepository : BaseRepository<Report>,IReportRepository
    {
      
        public ReportRepository(AppDbContext context) : base(context)
        {
        }

        // ✅ تنفيذ الـ method
        public async Task<bool> IncrementLikesAsync(int reportId)
        {
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE Reports SET LikesCount = LikesCount + 1 WHERE ReportID = {0}",
                reportId
            );

            return rowsAffected > 0;
        }

       
        public async Task<IEnumerable<Report>> GetMostLikedReportsAsync(int count)
        {
            return await _dbSet
                .Include(r => r.User)
                .Include(r => r.City)
                .OrderByDescending(r => r.LikesCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsByCityAsync(int cityId)
        {
            return await _dbSet
                .Include(r => r.User)
                .Where(r => r.User.CityId == cityId)
                .ToListAsync();
        }
    }
}
