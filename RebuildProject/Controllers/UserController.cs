using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using BusinceLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllWithIncludeAsync(u =>u.City);
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await _userService.GetAllWithIncludeAsync(u => u.City);
            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var createdUser = await _userService.AddAsync(createUserDto);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, createdUser);
        }

        // PUT: api/User/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            int editorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string editorRole = User.FindFirst(ClaimTypes.Role).Value;

         
            if (editorRole == "User" && editorId != id)
                return Forbid(); 

            

          
            var result = await _userService.UpdateAsync(id, updateUserDto);

            if (!result)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            int editorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            string editorRole = User.FindFirst(ClaimTypes.Role).Value;

           
            if (editorRole == "User")
            {
                if (editorId != id)
                    return Forbid(); 

                
            }

          
            if (editorRole == "Admin")
                return Forbid();

          
            if (editorRole == "SuperAdmin")
                return Forbid();

            var deleted = await _userService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResponse = await _userService.LoginAsync(loginDto);

            if (loginResponse == null)
                return Unauthorized("Invalid email or password");

            return Ok(loginResponse);
        }

        [HttpPatch("update-role")]
        [Authorize]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto dto)
        {
            //take id from token
            int editorId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value
            );

            var result = await _userService.UpdateUserRoleAsync(editorId, dto);

            if (!result)
                return Forbid();

            return Ok("Role updated successfully");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPatch("ban")]
        public async Task<IActionResult> BanUser(BanUserDto dto)
        {
            int editorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _userService.BanUserAsync(editorId, dto);

            if (!result)
                return Forbid(); 

            return Ok("User banned successfully");
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
                return Unauthorized("UserId claim not found in token.");

            if (!int.TryParse(claim.Value, out int userId))
                return Unauthorized("Invalid UserId claim in token.");

            var success = await _userService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            if (!success)
                return BadRequest("Current password is incorrect.");

            return Ok("Password changed successfully.");
        }
    }
}
