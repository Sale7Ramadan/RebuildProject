using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class CreateReportsLikeDto
    {
        public int ReportId { get; set; }
    }
    public class ReportsLikeDto
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }   // مهم جداً
        public int ReportId { get; set; }
    }
    public class UpdateReportLikesDto
    {
      
    }

}
