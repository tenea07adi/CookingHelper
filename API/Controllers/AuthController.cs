using API.Controllers.ActionFilters;
using API.Helpers;
using API.Models.DBModels;
using API.Models.DTOs.AuthDTOs;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IGenericRepo<UserDBM> _userRepo;
        private readonly IGenericRepo<UserInviteDBM> _userInviteRepo;

        public AuthController(IConfiguration configuration, IGenericRepo<UserDBM> userRepo, IGenericRepo<UserInviteDBM> userInviteRepo)
        {
            _configuration = configuration;
            _userRepo = userRepo;
            _userInviteRepo = userInviteRepo;
        }

        [HttpPost("login")]
        public IActionResult LogIn(LogInDTO logInData)
        {
            string invalidMessage = "Invalid credentials!";

            var dbUser = GetUserByEmail(logInData.Email);

            if (dbUser == null)
            {
                return Unauthorized(invalidMessage);
            }

            if (!CryptographyHelper.IsCorrectPassword(logInData.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                return Unauthorized(invalidMessage);
            }

            var response = new JwtTokenContainerDTO()
            {
                JwtToken = AuthHelper.GenerateJWTToken(dbUser, _configuration["Security:Issuer"], _configuration["Security:SecretKey"], Int32.Parse(_configuration["Security:ValidityInDays"]))
            };

            ResetLastLogIn(dbUser);

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO registerData)
        {
            var invaildMessage = "Invalid code or email.";

            var invite = _userInviteRepo.Get(c => c.Code == registerData.Code).FirstOrDefault();

            if (invite == null)
            {
                return BadRequest(invaildMessage);
            }

            if(invite.ValidUntil < DateTime.UtcNow)
            {
                _userInviteRepo.Delete(invite.Id);
                return BadRequest("Invite expired!");
            }

            if(registerData.Email != invite.Email)
            {
                return BadRequest(invaildMessage);
            }
            
            if (!CryptographyHelper.IsSafePassword(registerData.Password))
            {
                return BadRequest("The password needs to contains more then 5 characters, a high case character, a lowercase character, a digit and a special character.");
            }

            var passwordSalt = CryptographyHelper.GeneratePasswordSalt();
            var passwordHash = CryptographyHelper.HashPassword(registerData.Password, passwordSalt);

            var user = new UserDBM() { 
                Email = registerData.Email,
                DisplayName = registerData.DisplayName,
                Role = invite.Role,
                IsActive = true,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            _userRepo.Add(user);

            _userInviteRepo.Delete(invite.Id);

            return Ok();
        }

        [HttpPost("user/invite")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult InviteUser(RequestUserInviteDTO inviteData)
        {
            if (string.IsNullOrEmpty(inviteData.Email))
            {
                return BadRequest("Invalid email!");
            }

            if(_userInviteRepo.Get(c=>c.Email == inviteData.Email).Count() > 0)
            {
                return BadRequest("Invite already exist!");
            }

            if (GetUserByEmail(inviteData.Email) != null)
            {
                return BadRequest("Invalid email!");
            }

            if (!Enum.IsDefined(inviteData.Role))
            {
                return BadRequest("Invalid role!");
            }

            var validity = 7;

            try
            {
                validity = Int32.Parse(_configuration["AppConfigurations:UserInviteValidityInDays"]);
            }
            catch (Exception ex) { }

            var invite = new UserInviteDBM()
            {
                Email = inviteData.Email,
                Code = CryptographyHelper.GenerateRandomString(10),
                Role = inviteData.Role,
                ValidUntil = DateTime.UtcNow.AddDays(validity),
            };

            _userInviteRepo.Add(invite);

            return Ok(invite);
        }

        [HttpPost("user/ChangePassword")]
        public IActionResult ChangePassword([FromHeader] string authToken, ChangePasswordDTO changePasswordData)
        {
            if(authToken == null)
            {
                return Unauthorized();
            }

            var email = AuthHelper.GetEmailFromJwtToken(authToken);

            if(email == null)
            {
                return NotFound();
            }

            var user = GetUserByEmail(email);

            if(user == null)
            {
                return NotFound();
            }

            if (!CryptographyHelper.IsCorrectPassword(changePasswordData.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorect old password!");
            }

            if (!CryptographyHelper.IsSafePassword(changePasswordData.NewPassword))
            {
                return BadRequest("The password needs to contains more then 5 characters, a high case character, a lowercase character, a digit and a special character.");
            }

            user.PasswordHash = CryptographyHelper.HashPassword(changePasswordData.NewPassword, user.PasswordSalt);
            user.LastLogInReset = DateTime.UtcNow;

            _userRepo.Update(user);

            return Ok();
        }

        [HttpGet("user/activate/{userEmail}")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult ActivateUser(string userEmail)
        {
            var dbUser = GetUserByEmail(userEmail);

            if(dbUser == null)
            {
                return NotFound();
            }

            dbUser.IsActive = true;

            return Ok();
        }

        [HttpGet("user/deactivate/{userEmail}")]
        [AuthActionFilterAttribute(Models.DBModels.Roles.Admin)]
        public IActionResult DeactivateUser(string userEmail)
        {
            var dbUser = GetUserByEmail(userEmail);

            if (dbUser == null)
            {
                return NotFound();
            }

            dbUser.IsActive = false;

            return Ok();
        }

        private UserDBM GetUserByEmail(string email)
        {
            return _userRepo.Get(c => c.Email == email).FirstOrDefault();
        }

        private void ResetLastLogIn(UserDBM user)
        {
            user.LastLogInMoment = DateTime.UtcNow;
            _userRepo.Update(user);
        }
    }
}
