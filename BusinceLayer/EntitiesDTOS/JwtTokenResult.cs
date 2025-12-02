using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class JwtTokenResult
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
