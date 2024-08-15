using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
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
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetByUsername), new { username = user.UserName }, user);
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username, User user)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser == null)
                return NotFound();

            if (username != user.UserName)
                return BadRequest("Username does not match.");

            await _userRepository.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser == null)
                return NotFound();

            await _userRepository.DeleteAsync(existingUser.Id);
            return NoContent();
        }
    }
}
