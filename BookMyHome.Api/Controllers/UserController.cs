using BookMyHome.Application.Interfaces;
using BookMyHome.Domain;
using Microsoft.AspNetCore.Mvc;
using BookMyHome.Application.Dtos;

namespace BookMyHome.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                UserID = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                AccountType = dto.AccountType,
                Password = dto.Password // Consider hashing this!
            };

            await _userRepository.CreateAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserID }, user);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto updatedUser)
        {
            var user = new User
            {
                UserID = id,
                UserName = updatedUser.UserName,
                Email = updatedUser.Email,
                AccountType = updatedUser.AccountType
                // Password is not updated here
            };
            var success = await _userRepository.UpdateAsync(id, user);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var success = await _userRepository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
