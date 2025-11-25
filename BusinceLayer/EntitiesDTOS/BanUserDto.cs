using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.EntitiesDTOS
{
    public class BanUserDto
    {
        public int TargetUserId { get; set; }
        public string Reason { get; set; } 
    }

}
