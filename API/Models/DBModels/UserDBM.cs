using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserDBM : BaseDBM
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string DisplayName { get; set; }
        
    }
}
