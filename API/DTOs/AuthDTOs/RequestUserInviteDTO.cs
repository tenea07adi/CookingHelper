using Core.Entities.Persisted;

namespace API.DTOs.AuthDTOs
{
    public class RequestUserInviteDTO
    {
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
