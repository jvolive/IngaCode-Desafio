using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Services;
using IngaCode.Domain.Interfaces;
using IngaCode.Api.DTOs;

namespace IngaCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null || !VerifyPassword(user.Password, request.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = JwtTokenService.GenerateToken(user);
            return Ok(token);
        }

        private bool VerifyPassword(string storedPassword, string inputPassword)
        {
            return storedPassword == inputPassword;
        }
    }
}
