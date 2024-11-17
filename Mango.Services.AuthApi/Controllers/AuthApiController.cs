using Mango.Services.AuthApi.Models.Dto;
using Mango.Services.AuthApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Mango.Services.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto responseDto;

        public AuthApiController(IAuthService authService)
        {
           _authService = authService;
            responseDto = new();
           

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await _authService.Register(registrationRequestDto);
            if(errorMessage.IsNullOrEmpty())
            {
                responseDto.IsSuccess = true;
            }
            else
            {
               
                responseDto.IsSuccess = false;
                responseDto.Message = errorMessage;
            }

            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authService.Login(loginRequestDto);
            if(result.User == null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "user name or password not matched";
                return BadRequest(responseDto);
            }
            responseDto.IsSuccess = true;
            responseDto.Result = result;
            return Ok(responseDto); 
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto regRequestDto)
        {
            var result = await _authService.AssignRole(regRequestDto.Email,regRequestDto.Role.ToUpper());
            if (!result)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "error found";
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }
    }
}
