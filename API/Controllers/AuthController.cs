using API.Controllers.ActionFilters;
using API.DTOs.AuthDTOs;
using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult LogIn(LogInDTO logInData)
        {
            string invalidMessage = "Invalid credentials!";

            int maxLoginAtteptsStreak = Int32.Parse(_configuration["Security:MaxLoginAtteptsStreak"] ?? "0");

            var token = _authService.LogIn(
                logInData.Email, 
                logInData.Password, 
                maxLoginAtteptsStreak,
                _configuration["Security:Issuer"],
                _configuration["Security:SecretKey"],
                Int32.Parse(_configuration["Security:ValidityInDays"])
                );

            var response = new JwtTokenContainerDTO()
            {
                JwtToken = token
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerData)
        {
            _authService.Register(registerData.Code, registerData.Email, registerData.DisplayName, registerData.Password);

            return Ok();
        }

        [HttpPost("user/invite")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult InviteUser(RequestUserInviteDTO inviteData)
        {
            var userInviteValidityInDays = _configuration["AppConfigurations:UserInviteValidityInDays"];

            var invite = _authService.InviteUser(
                inviteData.Email, 
                inviteData.Role, 
                userInviteValidityInDays != null ? Int32.Parse(userInviteValidityInDays) : default);

            return Ok(invite);
        }

        [HttpPost("user/ChangePassword")]
        public IActionResult ChangePassword([FromHeader] string authToken, ChangePasswordDTO changePasswordData)
        {
            if(authToken == null)
            {
                return Unauthorized();
            }

            _authService.ChangePassword(authToken, changePasswordData.OldPassword, changePasswordData.NewPassword);

            return Ok();
        }

        [HttpGet("user/activate/{userEmail}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult ActivateUser(string userEmail)
        {
            _authService.ActivateUser(userEmail);

            return Ok();
        }

        [HttpGet("user/deactivate/{userEmail}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult DeactivateUser(string userEmail)
        {
            _authService.DeactivateUser(userEmail);

            return Ok();
        }

        [HttpGet("user/ResetLoginAttepts/{userEmail}")]
        [AuthActionFilterAttribute(Roles.Admin)]
        public IActionResult ResetLoginAtteptsStreak(string userEmail)
        {
            _authService.ResetLoginAtteptsStreak(userEmail);

            return Ok();
        }
    }
}
