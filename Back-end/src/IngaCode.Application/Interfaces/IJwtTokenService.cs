using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
