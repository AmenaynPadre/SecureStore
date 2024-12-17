using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var serviceResponse = await _userService.RegisterAsync(registerDto);

            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            var serviceResponse = await _userService.LoginAsync(loginDto);
            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Data));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var serviceResponse = await _userService.GetUserByIdAsync(id);

            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<UserDto>.SuccessResponse(serviceResponse.Data));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var serviceResponse = await _userService.GetAllUsersAsync();

            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(serviceResponse.Data));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var serviceResponse = await _userService.UpdateUserAsync(userDto);

            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var serviceResponse = await _userService.DeleteUserAsync(id);

            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }
    }
}
