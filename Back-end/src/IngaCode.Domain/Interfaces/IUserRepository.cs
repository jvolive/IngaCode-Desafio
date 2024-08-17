using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces;
public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<bool> VerifyUserPasswordAsync(string username, string password);
}
