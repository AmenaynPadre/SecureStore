using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.DTOs;
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
            try
            {
                var result = await _userService.RegisterAsync(registerDto);
                return Ok(new { Message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            try
            {
                var result = await _userService.LoginAsync(loginDto);
                return Ok(new { Message = "Login successful!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
