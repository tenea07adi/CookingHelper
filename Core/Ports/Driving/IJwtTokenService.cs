using Core.Entities.Persisted;
using System.Security.Claims;

namespace Core.Ports.Driving
{
    public interface IJwtTokenService
    {
        public bool ValidateToken(string jwtToken, string issuer, string secretKey, Roles requiredRole);
        public string GenerateJWTToken(User user, string issuer, string secretKey, int validityDays);
        public string GetEmailFromJwtToken(string jwtToken);
        public Roles GetRoleFromJwtToken(string jwtToken);
        public string GetClaimFromJwtToken(string jwtToken, string claim);
        public IEnumerable<Claim> GetClaimsFromJwtToken(string jwtToken);
    }
}
