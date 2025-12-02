using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepositories<User> _userRepo;
        private readonly IBaseRepositories<RefreshToken> _refreshTokenRepo;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(
            IBaseRepositories<User> userRepo,
            IBaseRepositories<RefreshToken> refreshTokenRepo,
            IMapper mapper,
            IJwtService jwtService)
        {
            _userRepo = userRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _mapper = mapper;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<User>();
        }

        // ================================
        // REGISTER
        // ================================
        public async Task<UserDto> RegisterAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                CityId = dto.CityId
            };

            // Hash password
            user.PassHash = _passwordHasher.HashPassword(user, dto.Password);

            var savedUser = await _userRepo.AddAsync(user);

            return _mapper.Map<UserDto>(savedUser);
        }


        // ================================
        // LOGIN
        // ================================
        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = (await _userRepo.GetAllAsync())
                .FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return null;

            if (user.IsBanned)
                return null;

            var passCheck = _passwordHasher.VerifyHashedPassword(user, user.PassHash, loginDto.Password);

            if (passCheck == PasswordVerificationResult.Failed)
                return null;

            // Generate Access Token
            var token = _jwtService.GenerateToken(user);

            // Generate Refresh Token
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                UserId = user.UserId
            };

            await _refreshTokenRepo.AddAsync(refreshToken);

            return new LoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token.Token,
                RefreshToken = refreshToken.Token,
                ExpiresOn = token.ExpiresOn
            };
        }


        // ================================
        // REFRESH TOKEN
        // ================================
        public async Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = (await _refreshTokenRepo.GetAllAsync())
                .FirstOrDefault(t => t.Token == refreshToken);

            if (storedToken == null)
                return null;

            if (!storedToken.IsActive)
                return null;

            var user = await _userRepo.GetByIdAsync(storedToken.UserId);

            if (user == null)
                return null;

            // create new access token
            var newAccessToken = _jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = newAccessToken.Token,
                RefreshToken = storedToken.Token, // same refresh token
                ExpiresOn = newAccessToken.ExpiresOn
            };
        }
    }
}
