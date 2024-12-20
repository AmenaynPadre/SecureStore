using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureStore1.API.Data.Entities;
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

        public async Task<ServiceResponse<string>> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ServiceResponse<string>.FailureResponse("User not found.");
            }

            await _userRepository.DeleteAsync(userId);
            return ServiceResponse<string>.SuccessResponse(null, "User deleted successfully.");
        }

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return ServiceResponse<IEnumerable<UserDto>>.FailureResponse("No users found.");
            }

            var userDtos = users.Select(user => _mapper.Map<UserDto>(user));
            return ServiceResponse<IEnumerable<UserDto>>.SuccessResponse(userDtos, "Users retrieved successfully.");
        }

        public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ServiceResponse<UserDto>.FailureResponse("User not found.");
            }
            UserDto dto = _mapper.Map<UserDto>(user);
            return ServiceResponse<UserDto>.SuccessResponse(dto, "User retrieved successfully.");
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginUserDto loginDto)
        {   
            var user = await _userRepository.GetUserByUserNameAsync(loginDto.UserName);
            if (user == null)
            {
                return ServiceResponse<string>.FailureResponse("Invalid username or password.");
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return ServiceResponse<string>.FailureResponse("Invalid username or password.");
            }

            var token = await GenerateJwtToken(user);

            return ServiceResponse<string>.SuccessResponse(token, "Login successful.");
        }
        public async Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = await _userRepository.GetUserByUserNameAsync(registerUserDto.UserName);
            if (existingUser != null)
            {
                return ServiceResponse<string>.FailureResponse("User already exists.");
            }

            var role = await _roleRepository.FindByNameAsync("User");
            if (role == null)
            {
                return ServiceResponse<string>.FailureResponse("Role not found.");
            }

            var user = _mapper.Map<User>(registerUserDto);

            user.Roles = new List<Role> { role };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);

            await _userRepository.AddAsync(user);

            return ServiceResponse<string>.SuccessResponse("User registered successfully.");
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
                    Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationInMinutes"])),
                    Audience = _configuration["Jwt:Audience"],
                    Issuer = _configuration["Jwt:Issuer"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);

        }

        public async Task<ServiceResponse<string>> UpdateUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(userDto.Id);

            if (existingUser == null)
            {
                return ServiceResponse<string>.FailureResponse("User not found.");
            }

            var updateResult = _userRepository.UpdateAsync(_mapper.Map(userDto, existingUser));

            if (updateResult == null)
            {
                return ServiceResponse<string>.FailureResponse("Failed to update user.");
            }

            return ServiceResponse<string>.SuccessResponse("User updated successfully.");
        }
    }
}
