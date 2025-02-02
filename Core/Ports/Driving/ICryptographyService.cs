
namespace Core.Ports.Driving
{
    public interface ICryptographyService
    {
        public bool IsSafePassword(string password);
        public bool IsCorrectPassword(string password, string passwrdHash, string passwordSalt);
        public string HashPassword(string password, string passwordSalt);
        public string GeneratePasswordSalt();
        public string GenerateRandomString(int length);
        public string GenerateRandomString(string alphabet, int length);
    }
}
