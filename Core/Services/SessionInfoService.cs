using Core.Entities.Persisted;
using Core.Ports.Driving;

namespace Core.Services
{
    public class SessionInfoService : ISessionInfoService
    {
        private User _currentUser;

        public User GetCurrentUserInfo()
        {
            return _currentUser;
        }

        public void SetCurrentUserInfo(User user)
        {
            _currentUser = user;
        }
    }
}
