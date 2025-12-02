using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IJwtService
    {
        JwtTokenResult GenerateToken(User user);
       // Task<TokenResponseModel> RefreshTokenAsync(string refreshToken)
    }
}
