using Core.Entities.Persisted;

namespace Core.Ports.Driving
{
    public interface ISessionInfoService
    {
        public void SetCurrentUserInfo(User user);
        public User? GetCurrentUserInfo();
    }
}
