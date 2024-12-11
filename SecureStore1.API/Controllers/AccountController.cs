using FluentValidation;
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
        private readonly IValidator<RegisterUserDto> _validator;

        public AccountController(IUserService userService , IValidator<RegisterUserDto> validator)
        {
            _userService = userService;
            _validator = validator;
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

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }
    }
}
