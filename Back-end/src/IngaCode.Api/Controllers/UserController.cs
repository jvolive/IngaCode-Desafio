using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;

namespace IngaCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            var result = await _userService.RegisterUserAsync(registerDto);

            if (result == "Username already taken")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserLoginDto loginDto)
        {
            var userResponse = await _userService.AuthenticateUserAsync(loginDto);

            if (userResponse == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(userResponse);
        }
    }
}
