using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.UserDTOs;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using IngaCode.Application.DTOs;
using AutoMapper;

namespace IngaCode.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto?> AuthenticateUserAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.UserName);
            if (user == null || !VerifyPassword(user.Password, loginDto.Password))
            {
                return null;
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<string> RegisterUserAsync(UserRegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(registerDto.UserName);
            if (existingUser != null)
            {
                return "Username already taken";
            }

            var newUser = _mapper.Map<User>(registerDto);
            newUser.Password = HashPassword(registerDto.Password);

            await _userRepository.AddAsync(newUser);

            return "User registered successfully";
        }

        private bool VerifyPassword(string storedPassword, string inputPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
