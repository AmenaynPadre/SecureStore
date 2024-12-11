using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureStore1.API.DTOs;
using SecureStore1.API.Helpers;
using SecureStore1.API.Models;
using SecureStore1.API.Repositories.Interfaces;
using SecureStore1.API.Services.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureStore1.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IPasswordHasher<User> passwordHasher, 
            IUserRepository userRepository, 
            IRoleRepository roleRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task DeleteUserAsync(int userId)
        {

            await _userRepository.DeleteAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            UserDto dto = _mapper.Map<UserDto>(user);
            return dto;
        }

        public async Task<string> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetUserByUserNameAsync(loginDto.UserName);
            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (user == null || verifyPassword == 0)
            {
                throw new Exception("something wrong");
            }

            var token = await GenerateJwtToken(user);

            return token;
        }
        public async Task<string> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var isUserExist = await _userRepository.GetUserByUserNameAsync(registerUserDto.UserName);
            if (isUserExist != null)
            {
                throw new Exception("user exist");
            }

            var role = await _roleRepository.FindByNameAsync("User");

            var user = _mapper.Map<User>(registerUserDto);

            // Hash the password and assign it to the PasswordHash property of the user
            user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);


            await _userRepository.AddAsync(user);

            return "ok";
        }

        public Task UpdateUserAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpirationInMinutes"])),
                    Audience = _configuration["Jwt:Audience"],
                    Issuer = _configuration["Jwt:Issuer"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);

        }
    }
}
