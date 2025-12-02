using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Interfaces
{
    public interface IUserService : IBaseService<User, UserDto, CreateUserDto, UpdateUserDto>
    {
        //Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
        Task<bool> UpdateUserRoleAsync(int editorId, UpdateRoleDto dto);
        Task<bool> BanUserAsync(int editorId, BanUserDto dto);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}
