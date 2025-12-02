using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsRevoked => RevokedOn != null;
        public bool IsActive => !IsRevoked && !IsExpired;

        // العلاقة مع المستخدم
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
