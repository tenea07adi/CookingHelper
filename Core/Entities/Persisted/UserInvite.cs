using Core.Entities.Abstract;

namespace Core.Entities.Persisted
{
    public class UserInvite : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
