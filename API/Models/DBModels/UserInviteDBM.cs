using API.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace API.Models.DBModels
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserInviteDBM : BaseDBM
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
