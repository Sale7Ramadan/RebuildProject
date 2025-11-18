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
using BusinceLayer.Interfaces;

namespace BusinceLayer.Services
{
    public class UserService : BaseService<User, UserDto, CreateUserDto, UpdateUserDto>,IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        public UserService(IBaseRepositories<User> repository, IMapper mapper, IJwtService jwtService)
            : base(repository, mapper)
        {
            _passwordHasher = new PasswordHasher<User>();
            _jwtService = jwtService;
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
        //public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        //{
           
        //    var user = (await _repository.GetAllAsync())
        //                .FirstOrDefault(u => u.Email == loginDto.Email);

        //    if (user == null)
        //        return null; 

          
        //    var result = _passwordHasher.VerifyHashedPassword(user, user.PassHash, loginDto.Password);

        //    if (result == PasswordVerificationResult.Failed)
        //        return null; 

          
        //    return _mapper.Map<UserDto>(user);
        //}


        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
{
    var user = (await _repository.GetAllAsync())
                .FirstOrDefault(u => u.Email == loginDto.Email);

    if (user == null)
        return null;

    var result = _passwordHasher.VerifyHashedPassword(user, user.PassHash, loginDto.Password);

    if (result == PasswordVerificationResult.Failed)
        return null;

    // توليد JWT
    var token = _jwtService.GenerateToken(user);

    // إنشاء الـResponse DTO
    var response = new LoginResponseDto
    {
        User = _mapper.Map<UserDto>(user),
        Token = token
    };

    return response;
}

    }
}
