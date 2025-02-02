using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public Roles Role { get; set; }

        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public int LoginAttemptsStreak { get; set; }

        public DateTime LastLogInMoment { get; set; }
        public DateTime LastLogInReset { get; set; }
    }

    public enum Roles
    {
        Admin = 0,
        ReadOnly = 1
    }
}
