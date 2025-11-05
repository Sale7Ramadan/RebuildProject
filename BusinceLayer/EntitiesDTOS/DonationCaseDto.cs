using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    
        public class DonationCaseDto
        {
            public int DonationCaseId { get; set; }
            public decimal GoalAmount { get; set; }
            public decimal CollectedAmount { get; set; }
            public string Status { get; set; }
            public DateTime? OpenedAt { get; set; }
            public DateTime? ClosedAt { get; set; }

            public int ReportId { get; set; }
            public string ReportTitle { get; set; }
            public string ReportDescription { get; set; } 

            public decimal ProgressPercentage => GoalAmount > 0 ? (CollectedAmount / GoalAmount) * 100 : 0;
            public bool IsGoalReached => CollectedAmount >= GoalAmount;
        }
    public class CreateDonationCaseDto
    {
        //[Required(ErrorMessage = "معرّف البلاغ مطلوب.")]
        public int ReportId { get; set; }

        [Required(ErrorMessage = "المبلغ المستهدف مطلوب.")]
        //[Range(100.0, 1000000.00, ErrorMessage = "المبلغ المستهدف يجب أن يكون بين 100 و 1,000,000.")]
        public decimal GoalAmount { get; set; }
    }

    public class UpdateDonationCaseDto
    {
        //[Range(100.0, 1000000.00, ErrorMessage = "المبلغ المستهدف يجب أن يكون بين 100 و 1,000,000.")]
        public decimal? GoalAmount { get; set; } 

        public string? Status { get; set; } 
    }

}
