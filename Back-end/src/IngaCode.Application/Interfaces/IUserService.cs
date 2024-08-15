using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> AuthenticateUserAsync(string username, string password);
        Task<string> RegisterUserAsync(string username, string password);
    }
}
