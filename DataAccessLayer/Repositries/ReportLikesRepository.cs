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
    public class ReportLikesRepository : BaseRepository<ReportsLikes>, IReportLikesRepository
    {
        public ReportLikesRepository(AppDbContext context) : base(context) { }

        public async Task<bool> HasUserLikedAsync(int userId, int reportId)
        {
            return await _dbSet.AnyAsync(l => l.UserId == userId && l.ReportId == reportId);
        }

        public async Task<ReportsLikes> GetUserLikeAsync(int userId, int reportId)
        {
            return await _dbSet.FirstOrDefaultAsync(l => l.UserId == userId && l.ReportId == reportId);
        }

        public async Task<int> GetLikesCountAsync(int reportId)
        {
            return await _dbSet.CountAsync(l => l.ReportId == reportId);
        }

        public async Task<IEnumerable<ReportsLikes>> GetLikesWithUsersAsync(int reportId)
        {
            return await _dbSet
                .Include(l => l.User)
                .Where(l => l.ReportId == reportId)
                .ToListAsync();
        }
    }

}
