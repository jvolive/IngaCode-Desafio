using IngaCode.Application.DTOs;
using IngaCode.Application.DTOs.UserDTOs;

namespace IngaCode.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> AuthenticateUserAsync(UserLoginDto loginDto);
        Task<string> RegisterUserAsync(UserRegisterDto registerDto);
    }
}
