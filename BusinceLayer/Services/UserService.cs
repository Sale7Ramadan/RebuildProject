using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinceLayer.Services
{
    public class UserService : BaseService<User, UserDto, CreateUserDto, UpdateUserDto>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IBaseRepositories<User> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public override async Task<UserDto> AddAsync(CreateUserDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Password))
            {
                throw new ArgumentException("Password is required");
            }

            var user = new User
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Role = createDto.Role,
                PhoneNumber = createDto.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            user.PassHash = _passwordHasher.HashPassword(user, createDto.Password);

            var savedUser = await _repository.AddAsync(user);

            return _mapper.Map<UserDto>(savedUser);
        }
        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
           
            var user = (await _repository.GetAllAsync())
                        .FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return null; 

          
            var result = _passwordHasher.VerifyHashedPassword(user, user.PassHash, loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null; 

          
            return _mapper.Map<UserDto>(user);
        }
    }
}
