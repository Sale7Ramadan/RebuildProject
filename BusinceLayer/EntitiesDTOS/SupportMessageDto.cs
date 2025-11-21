using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class SupportMessageDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public bool IsFromAdmin { get; set; }
    }

    public class CreateSupportMessageDto
    {
        public int TicketId { get; set; }
        public int SenderId { get; set; }
        public string Message { get; set; }
        public bool IsFromAdmin { get; set; }
    }

    public class UpdateSupportMessageDto
    {
        public string Message { get; set; }
    }

}
