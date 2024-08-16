using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserDto userDto);
    }
}
