using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !VerifyPassword(user.Password, password))
            {
                return null;
            }

            return user;
        }

        public async Task<string> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
            {
                return "Username already taken";
            }

            var newUser = new User
            {
                UserName = username,
                Password = HashPassword(password),
            };

            await _userRepository.AddAsync(newUser);

            return "User registered successfully";
        }

        private bool VerifyPassword(string storedPassword, string inputPassword)
        {
            var hashedInputPassword = HashPassword(inputPassword);
            return storedPassword == hashedInputPassword;
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
