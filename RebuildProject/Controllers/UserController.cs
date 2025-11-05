using BusinceLayer.EntitiesDTOS;
using BusinceLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RebuildProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBaseService<User, UserDto, CreateUserDto, UpdateUserDto> _userService;

        public UserController(IBaseService<User, UserDto, CreateUserDto, UpdateUserDto> userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateAsync(id, updateUserDto);
            if (!result)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }
    }
}
