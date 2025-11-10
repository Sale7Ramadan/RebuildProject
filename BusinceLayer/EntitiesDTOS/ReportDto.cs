using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string UserName { get; set; }        
        public string CategoryName { get; set; }    
        public string CityName { get; set; }        

        public int CommentCount { get; set; }

        public List<ReportImageDto> ReportImages { get; set; } = new List<ReportImageDto>();

        public decimal? TotalCollectedAmount { get; set; }
        public bool HasDonationCase { get; set; }
    }
    public class CreateReportDto
    {
        //[Required(ErrorMessage = "عنوان البلاغ مطلوب.")]
        //[StringLength(200, ErrorMessage = "العنوان طويل جداً، الحد الأقصى 200 حرف.")]
        public string Title { get; set; }

        //[StringLength(2000, ErrorMessage = "الوصف طويل جداً، الحد الأقصى 2000 حرف.")]
        public string? Description { get; set; }

        //[Range(0.0, 1000000.00, ErrorMessage = "التكلفة التقديرية يجب أن تكون قيمة موجبة.")]
        public decimal? EstimatedCost { get; set; }

        //[Required(ErrorMessage = "يجب اختيار التصنيف.")]
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "يجب اختيار المدينة.")]
        public int CityId { get; set; }

        public List<ReportImageDto> ReportImages { get; set; } = new List<ReportImageDto>();
    }

    public class UpdateReportDto
    {
        //[Required(ErrorMessage = "عنوان البلاغ مطلوب.")]
        //[StringLength(200, ErrorMessage = "العنوان طويل جداً، الحد الأقصى 200 حرف.")]
        public string Title { get; set; }

        //[StringLength(2000, ErrorMessage = "الوصف طويل جداً، الحد الأقصى 2000 حرف.")]
        public string? Description { get; set; }

        //[Range(0.0, 1000000.00, ErrorMessage = "التكلفة التقديرية يجب أن تكون قيمة موجبة.")]
        public decimal? EstimatedCost { get; set; }

        //[Required(ErrorMessage = "يجب اختيار التصنيف.")]
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "يجب اختيار المدينة.")]
        public int CityId { get; set; }
    }


}
