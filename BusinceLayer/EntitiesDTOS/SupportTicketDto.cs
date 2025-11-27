using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class SupportTicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
    }

    public class CreateSupportTicketDto
    {
        public string Title { get; set; }
       // public int UserId { get; set; }
        public int CityId { get; set; }
    }

    public class UpdateSupportTicketDto
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }

}
