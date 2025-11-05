using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class DontationDto
    {
        public class DonationDto
        {
            public int DonationsId { get; set; }
            public decimal Amount { get; set; }
            public DateTime? DonatedAt { get; set; }

            public string DonationCaseTitle { get; set; }

            public string DonorDisplayName { get; set; }
        }


        public class CreateDonationDto
        {
            //[Required(ErrorMessage = "مبلغ التبرع مطلوب.")]
            //[Range(0.01, 1000000.00, ErrorMessage = "مبلغ التبرع يجب أن يكون بين 0.01 و 1,000,000.")]
            public decimal Amount { get; set; }

            //[Required(ErrorMessage = "يجب تحديد حالة التبرع.")]
            public int DonationCaseId { get; set; }

            // هذا الحقل اختياري، للمتبرعين المجهولين فقط
            //[StringLength(100, ErrorMessage = "طول الاسم يجب ألا يتجاوز 100 حرف.")]
            public string? DonorName { get; set; }
        }





    }
}
