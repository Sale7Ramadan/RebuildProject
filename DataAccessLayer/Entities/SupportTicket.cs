using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class SupportTicket
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Title { get; set; }
        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SupportMessage> Messages { get; set; }
    }
}
