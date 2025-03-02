using Core.Entities.Persisted;
using Core.Ports.Driving;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        public bool ValidateToken(string jwtToken, string issuer, string secretKey, Roles requiredRole)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) // The same key as the one that generate the token
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(jwtToken))
            {
                return false;
            }

            try
            {
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(jwtToken, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            Roles tokenRole = GetRoleFromJwtToken(jwtToken);

            // Lower roles are higher ranked
            if (tokenRole > requiredRole)
            {
                return false;
            }

            return true;
        }

        public string GenerateJWTToken(User user, string issuer, string secretKey, int validityDays)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.DisplayName),
                new Claim("role", ((int)user.Role).ToString()),
                new Claim("roleName", Enum.GetName(user.Role))
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(validityDays),
                issuer: issuer,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GetEmailFromJwtToken(string jwtToken)
        {
            return GetClaimFromJwtToken(jwtToken, "email");
        }

        public Roles GetRoleFromJwtToken(string jwtToken)
        {
            Roles role = Roles.ReadOnly;

            try
            {
                role = (Roles)Convert.ToInt32(GetClaimFromJwtToken(jwtToken, "role"));
            }
            catch { }

            return role;
        }

        public string GetClaimFromJwtToken(string jwtToken, string claim)
        {
            return GetClaimsFromJwtToken(jwtToken).Where(c => c.Type == claim).FirstOrDefault().Value;
        }

        public IEnumerable<Claim> GetClaimsFromJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.ReadJwtToken(jwtToken).Claims;
        }
    }
}
