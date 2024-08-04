using API.Models.DBModels;

namespace API.Models.DTOs.AuthDTOs
{
    public class RequestUserInviteDTO
    {
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
