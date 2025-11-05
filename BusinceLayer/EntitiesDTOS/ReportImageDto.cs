using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class ReportImageDto
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public int ReportId { get; set; }
    }

    public class CreateReportImageDto
    {
        //[Required(ErrorMessage = "رابط الصورة مطلوب.")]
        public string ImageUrl { get; set; }
        public int ReportId { get; set; }
    }
}
