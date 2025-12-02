using BusinceLayer.EntitiesDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
        Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken);
        Task<UserDto> RegisterAsync(CreateUserDto dto);
    }

}
