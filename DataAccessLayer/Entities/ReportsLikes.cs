using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ReportsLikes
    {
        public int ReportLikeId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ReportId { get; set; }
        public Report Report { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}
