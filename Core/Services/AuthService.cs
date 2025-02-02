using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IGenericRepo<User> _userRepo;
        private readonly IGenericRepo<UserInvite> _userInviteRepo;

        public AuthService(
            IJwtTokenService jwtTokenService,
            ICryptographyService cryptographyService,
            IGenericRepo<User> userRepo, 
            IGenericRepo<UserInvite> userInviteRepo)
        {
            _jwtTokenService = jwtTokenService;
            _cryptographyService = cryptographyService;
            _userRepo = userRepo;
            _userInviteRepo = userInviteRepo;
        }

        public string LogIn(string email, string password, 
            int maxLoginAtteptsStreak, string issuer, string secretKey, int validityInDays)
        {
            var dbUser = GetUserByEmail(email);

            if (dbUser == null)
            {
                throw new UnauthorizedAccessException(Constants.Exceptions.InvalidCredentials);
            }
            
            if (dbUser.LoginAttemptsStreak > maxLoginAtteptsStreak || !dbUser.IsActive || !_cryptographyService.IsCorrectPassword(password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                AddLogInAttept(dbUser);

                throw new UnauthorizedAccessException(Constants.Exceptions.InvalidCredentials);
            }
            
            ResetLogInAttept(dbUser);

            var token = _jwtTokenService.GenerateJWTToken(dbUser, issuer, secretKey, validityInDays);

            ResetLastLogIn(dbUser);

            return token;
        }

        public void Register(string code, string email, string displayName, string password)
        {
            var invite = _userInviteRepo.Get(c => c.Code == code).FirstOrDefault();

            if (invite == null)
            {
                throw new ArgumentException(Constants.Exceptions.InvalidCodeOrEmail);
            }

            if (invite.ValidUntil < DateTime.UtcNow)
            {
                _userInviteRepo.Delete(invite.Id);
                throw new ArgumentException(Constants.Exceptions.InviteExpired);
            }

            if (email != invite.Email)
            {
                throw new ArgumentException(Constants.Exceptions.InvalidCodeOrEmail);
            }

            if (_cryptographyService.IsSafePassword(password))
            {
                throw new ArgumentException(Constants.Exceptions.NotSafePassword);
            }

            var passwordSalt = _cryptographyService.GeneratePasswordSalt();
            var passwordHash = _cryptographyService.HashPassword(password, passwordSalt);

            var user = new User()
            {
                Email = email,
                DisplayName = displayName,
                Role = invite.Role,
                IsActive = true,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            _userRepo.Add(user);

            _userInviteRepo.Delete(invite.Id);
        }

        public UserInvite InviteUser(string email, Roles role, int userInviteValidityInDays = -1)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(Constants.Exceptions.InvalidEmail);
            }

            if (_userInviteRepo.Get(c => c.Email == email).Count() > 0)
            {
                throw new ArgumentException(Constants.Exceptions.InviteAlreadyExists);
            }

            if (GetUserByEmail(email) != null)
            {
                throw new ArgumentException(Constants.Exceptions.InvalidEmail);
            }

            if (!Enum.IsDefined(role))
            {
                throw new ArgumentException(Constants.Exceptions.InvalidRole);
            }

            var validity = 7;

            if (userInviteValidityInDays != -1)
            {
                validity = userInviteValidityInDays;
            }

            var invite = new UserInvite()
            {
                Email = email,
                Code = _cryptographyService.GenerateRandomString(10),
                Role = role,
                ValidUntil = DateTime.UtcNow.AddDays(validity),
            };

            _userInviteRepo.Add(invite);

            return invite;
        }

        public void ChangePassword(string authToken, string oldPassword, string newPassword)
        {
            var email = _jwtTokenService.GetEmailFromJwtToken(authToken);

            if (email == null)
            {
                throw new ArgumentException(Constants.Exceptions.UserNotFound);
            }

            var user = GetUserByEmail(email);

            if (user == null)
            {
                throw new ArgumentException(Constants.Exceptions.UserNotFound);
            }

            if (!_cryptographyService.IsCorrectPassword(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new ArgumentException(Constants.Exceptions.IncorectOldPassword);
            }

            if (!_cryptographyService.IsSafePassword(newPassword))
            {
                throw new ArgumentException(Constants.Exceptions.NotSafePassword);
            }

            user.PasswordHash = _cryptographyService.HashPassword(newPassword, user.PasswordSalt);
            user.LastLogInReset = DateTime.UtcNow;

            _userRepo.Update(user);
        }

        public void ActivateUser(string userEmail)
        {
            var dbUser = GetUserByEmail(userEmail);

            if (dbUser == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.UserNotFound);
            }

            dbUser.IsActive = true;

            _userRepo.Update(dbUser);
        }

        public void DeactivateUser(string userEmail)
        {
            var dbUser = GetUserByEmail(userEmail);

            if (dbUser == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.UserNotFound);
            }

            dbUser.IsActive = false;

            _userRepo.Update(dbUser);
        }

        public void ResetLoginAtteptsStreak(string userEmail)
        {
            var dbUser = GetUserByEmail(userEmail);

            if (dbUser == null)
            {
                throw new KeyNotFoundException(Constants.Exceptions.UserNotFound);
            }

            ResetLogInAttept(dbUser);
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepo.Get(c => c.Email == email).FirstOrDefault();
        }

        private void ResetLastLogIn(User user)
        {
            user.LastLogInMoment = DateTime.UtcNow;
            _userRepo.Update(user);
        }

        private void AddLogInAttept(User user)
        {
            user.LoginAttemptsStreak++;
            _userRepo.Update(user);
        }

        private void ResetLogInAttept(User user)
        {
            user.LoginAttemptsStreak = 0;
            _userRepo.Update(user);
        }
    }
}
