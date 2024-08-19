using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = _tokenService.GenerateToken(loginDto);
        if (token == "")
        {
            return Unauthorized();
        }

        return new JsonResult(token);
    }
}
