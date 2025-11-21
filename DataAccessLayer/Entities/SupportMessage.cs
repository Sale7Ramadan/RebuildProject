using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class SupportMessage
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public SupportTicket Ticket { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public string Message { get; set; }
        public bool IsFromAdmin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
