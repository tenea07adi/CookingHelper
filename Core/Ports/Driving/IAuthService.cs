using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface IAuthService
    {
        public string LogIn(string email, string password,
            int maxLoginAtteptsStreak, string issuer, string secretKey, int validityInDays);
        public void Register(string code, string email, string displayName, string password);
        UserInvite InviteUser(string email, Roles role, int userInviteValidityInDays = -1);
        public void ChangePassword(string authToken, string oldPassword, string newPassword);
        public void ActivateUser(string userEmail);
        public void DeactivateUser(string userEmail);
        public void ResetLoginAtteptsStreak(string userEmail);
        public User? GetUserByEmail(string email);
    }
}
