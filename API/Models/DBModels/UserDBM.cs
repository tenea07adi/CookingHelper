using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserDBM : BaseDBM
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public Roles Role { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastLogInMoment { get; set; }
        public DateTime LastLogInReset { get; set; }
    }

    public enum Roles
    {
        Admin = 0,
        ReadOnly = 1
    }
}
