namespace IngaCode.Application.Interfaces;

public interface IAuthenticationService
{
    Task<string> AuthenticateAsync(string username, string password);
}
