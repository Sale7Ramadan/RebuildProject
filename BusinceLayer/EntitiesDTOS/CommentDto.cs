using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class CommentDto
    {

        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

       
        public int ReportId { get; set; }
    }
    public class CreateCommentDto
    {
        //[Required(ErrorMessage = "نص التعليق مطلوب.")]
        //[StringLength(1000, ErrorMessage = "التعليق طويل جداً، الحد الأقصى 1000 حرف.")]
        public string CommentText { get; set; }
      

        //[Required(ErrorMessage = "يجب تحديد البلاغ المرتبط بالتعليق.")]
        public int ReportId { get; set; }
    }

    public class UpdateCommentDto
    {
        //[Required(ErrorMessage = "نص التعليق مطلوب.")]
        //[StringLength(1000, ErrorMessage = "التعليق طويل جداً، الحد الأقصى 1000 حرف.")]
        public string CommentText { get; set; }
     

    }






}
