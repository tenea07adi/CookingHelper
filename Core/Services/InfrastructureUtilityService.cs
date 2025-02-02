using Core.Entities.Persisted;
using Core.Ports.Driven;
using Core.Ports.Driving;

namespace Core.Services
{
    public class InfrastructureUtilityService : IInfrastructureUtilityService
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IGenericEntityService<User> _userService;
        private readonly IAuthService _authService;
        private readonly IDataBaseService _dataBaseService;

        public InfrastructureUtilityService(
            ICryptographyService cryptographyService, 
            IGenericEntityService<User> userService,
            IAuthService authService,
            IDataBaseService dataBaseService)
        {
            _cryptographyService = cryptographyService;
            _userService = userService;
            _authService = authService;
            _dataBaseService = dataBaseService;
        }

        public string Init(string defaultUserEmail, string defaultUserName)
        {
            if (string.IsNullOrEmpty(defaultUserEmail) || string.IsNullOrEmpty(defaultUserName))
            {
                throw new ArgumentException(Constants.Exceptions.InvalidAppConfig);
            }

            string response = "";

            try
            {
                _dataBaseService.UpdateDatabase();
                response += "Database init success! \n";
            }
            catch (Exception ex)
            {
                response += "Error while database init! Error message: \n" + ex.Message;
                throw new Exception(response);
            }

            try
            {
                response += "Default user init success! Credentials: " + AddDefaultUser(defaultUserEmail, defaultUserName) + "\n";
            }
            catch (Exception ex)
            {
                response += "Error while default user init! Error message: \n" + ex.Message;
                throw new Exception(response);

            }

            return response;
        }

        public void RunDatabaseUpdate()
        {
            _dataBaseService.UpdateDatabase();
        }

        private string AddDefaultUser(string defaultUserEmail, string defaultUserName)
        {
            string randomGeneratedPassword = _cryptographyService.GenerateRandomString(6);

            if (_authService.GetUserByEmail(defaultUserEmail) != null)
            {
                throw new ArgumentException("User already initilized!");
            }

            User defaultUser = new User();
            defaultUser.Email = defaultUserEmail;
            defaultUser.DisplayName = defaultUserName;
            defaultUser.Role = Roles.Admin;
            defaultUser.PasswordSalt = _cryptographyService.GeneratePasswordSalt();
            defaultUser.PasswordHash = _cryptographyService.HashPassword(randomGeneratedPassword, defaultUser.PasswordSalt);

            _userService.Add(defaultUser);

            return $"Username: {defaultUser.Email}; Password: {randomGeneratedPassword}";
        }
    }
}
