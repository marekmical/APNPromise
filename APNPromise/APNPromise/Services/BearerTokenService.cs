using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using APNPromise.Configuration;

namespace APNPromise.Services
{
    public class BearerTokenService
    {
        public string GenerateBearerToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BearerTokenOptions.SigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiry = DateTimeOffset.Now.AddMinutes(15);
            var userClaims = GetClaimsForUser(1);

            var securityToken = new JwtSecurityToken(
                issuer: BearerTokenOptions.Issuer,
                audience: BearerTokenOptions.Audience,
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: expiry.DateTime,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private IEnumerable<Claim> GetClaimsForUser(int userId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, "test@dev.com"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, "dev"));

            return claims;
        }
    }
}
