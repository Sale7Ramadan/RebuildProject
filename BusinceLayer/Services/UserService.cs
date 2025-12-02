using AutoMapper;
using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinceLayer.Services
{
    public class UserService : BaseService<User, UserDto, CreateUserDto, UpdateUserDto>, IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IBaseRepositories<User> repository, IMapper mapper, IJwtService jwtService,IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper)
        {
            _passwordHasher = new PasswordHasher<User>();
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<UserDto> AddAsync(CreateUserDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Password))
                throw new ArgumentException("Password is required");

            // افتراضي: كل المستخدمين الجدد يكونون User
            var roleToAssign = "User";

            // إذا من داخل Context لديك معلومات عن المستخدم الحالي (Admin/SuperAdmin)
            // مثال: User.Identity أو Claims
            // شرط: إذا المستخدم الحالي Admin أو SuperAdmin فقط يسمح بتعيين الدور
            if (_httpContextAccessor.HttpContext != null)
            {
                var currentUserRole = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                if (currentUserRole == "Admin" || currentUserRole == "SuperAdmin")
                {
                    if (!string.IsNullOrWhiteSpace(createDto.Role))
                        roleToAssign = createDto.Role; // المسؤول يمكنه تغيير الدور
                }
            }

            var user = new User
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                Role = roleToAssign, // مهم ✅
                PhoneNumber = createDto.PhoneNumber,
                CreatedAt = DateTime.Now,
                CityId = createDto.CityId
            };

            user.PassHash = _passwordHasher.HashPassword(user, createDto.Password);

            var savedUser = await _repository.AddAsync(user);

            return _mapper.Map<UserDto>(savedUser);
        }



//        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
//{
//    var user = (await _repository.GetAllAsync())
//                .FirstOrDefault(u => u.Email == loginDto.Email);

//    if (user == null)
//        return null;
//    if(user.IsBanned)
//        return null;

//            var result = _passwordHasher.VerifyHashedPassword(user, user.PassHash, loginDto.Password);

//    if (result == PasswordVerificationResult.Failed)
//        return null;

//    // توليد JWT
//    var token = _jwtService.GenerateToken(user);
      
//    // إنشاء الـResponse DTO
//    var response = new LoginResponseDto
//    {
        
//        User = _mapper.Map<UserDto>(user),
//        Token = token
//    };

//    return response;
//}

        public async Task<bool> UpdateUserRoleAsync(int editorId, UpdateRoleDto dto)
        {
            // 1.get editor user
            var editorUser = await _repository.GetByIdAsync(editorId);
            if (editorUser == null)
                return false;

            // 2. check  SuperAdmin
            if (editorUser.Role != "SuperAdmin")
                return false;

            // 3. get target user
            var targetUser = await _repository.GetByIdAsync(dto.TargetUserId);
            if (targetUser == null)
                return false;

            // update role
            targetUser.Role = dto.NewRole;

            // save changes
            await _repository.UpdateAsync(targetUser);

            return true;
        }
        public async Task<bool> BanUserAsync(int editorId, BanUserDto dto)
        {
          
            var editorUser = await _repository.GetByIdAsync(editorId);
            if (editorUser == null)
                return false;

         
            if (editorUser.Role != "Admin" && editorUser.Role != "SuperAdmin")
                return false;

  
            var targetUser = await _repository.GetByIdAsync(dto.TargetUserId);
            if (targetUser == null)
                return false;


            if (editorUser.Role == "Admin" && targetUser.Role == "SuperAdmin")
                return false;


            targetUser.IsBanned = true; 
            targetUser.BanReason = dto.Reason;

            await _repository.UpdateAsync(targetUser);

            return true;
        }
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

           
            var result = _passwordHasher.VerifyHashedPassword(user, user.PassHash, currentPassword);
            if (result == PasswordVerificationResult.Failed)
                return false;

           
            user.PassHash = _passwordHasher.HashPassword(user, newPassword);

            await _repository.UpdateAsync(user);
            return true;
        }



    }
}
