using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(LoginDto loginDto);
}
