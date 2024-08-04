using API.DataBase;
using API.Helpers;
using API.Models.DBModels;
using API.Repository.Generics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DataBaseContext _dataBaseContext;
        private readonly IGenericRepo<UserDBM> _userRepo;

        public UtilityController(IConfiguration configuration, DataBaseContext dataBaseContext, IGenericRepo<UserDBM> userRepo)
        {
            _configuration= configuration;
            _dataBaseContext = dataBaseContext;
            _userRepo = userRepo;
        }

        [HttpGet("Init")]
        public IActionResult Init()
        {
            string response = "";

            try
            {
                UpdateDatabase();
                response += "Database init success! \n";
            }
            catch (Exception ex)
            {
                response += "Error while database init! Error message: \n" + ex.Message;
                return BadRequest(response);
            }

            try
            {
                response += "Default user init success! Credentials: " + AddDefaultUser() + "\n";
            }
            catch (Exception ex)
            {
                response += "Error while default user init! Error message: \n" + ex.Message;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("UpdateDatabase")]
        public IActionResult RunDatabaseUpdate()
        {
            try
            {
                UpdateDatabase();
                return Ok("Success!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void UpdateDatabase()
        {
            _dataBaseContext.Database.Migrate();
        }

        private string AddDefaultUser()
        {
            string defaultUserEmail = _configuration["AppConfigurations:DefaultUserEmail"];
            string defaultUserName = _configuration["AppConfigurations:DefaultUserDisplayName"];

            string randomGeneratedPassword = CryptographyHelper.GenerateRandomString(6);

            if (_userRepo.Get().Where(c => c.Email == defaultUserEmail).Any())
            {
                throw new Exception("User already initilized!");
            }

            UserDBM defaultUser = new UserDBM();
            defaultUser.Email = defaultUserEmail;
            defaultUser.DisplayName = defaultUserName;
            defaultUser.Role = Roles.Admin;
            defaultUser.PasswordSalt = CryptographyHelper.GeneratePasswordSalt();
            defaultUser.PasswordHash = CryptographyHelper.HashPassword(randomGeneratedPassword, defaultUser.PasswordSalt);

            _userRepo.Add(defaultUser);

            return $"Username: {defaultUser.Email}; Password: {randomGeneratedPassword}";
        }
    }
}
