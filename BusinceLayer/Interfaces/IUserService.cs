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
        Task<UserDto?> LoginAsync(LoginDto loginDto);
    }
}
