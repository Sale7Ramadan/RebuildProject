using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int ReportsCount { get; set; }
        public int CommentsCount { get; set; }
        public int DonationsCount { get; set; }

    }
    public class CreateUserDto
    {
        //[Required(ErrorMessage = "الاسم الأول مطلوب.")]
        //[StringLength(50)]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "الاسم الأخير مطلوب.")]
        //[StringLength(50)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        //[EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        //[StringLength(100, MinimumLength = 8, ErrorMessage = "كلمة المرور يجب أن تكون بين 8 و 100 حرف.")]
        public string Password { get; set; } // نستقبل كلمة المرور كنص عادي

        public string Role { get; set; } = "User"; // القيمة الافتراضية للدور
        //[StringLength(20)]
        public string? PhoneNumber { get; set; }
    }


    public class UpdateUserDto
    {
        //[Required(ErrorMessage = "الاسم الأول مطلوب.")]
        //[StringLength(50)]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "الاسم الأخير مطلوب.")]
        //[StringLength(50)]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        //[EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة.")]
        public string Email { get; set; }

        //[StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}

