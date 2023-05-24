using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Utils.CrossCuttingConcerns.Utilities
{
    public static class JwtToken
    {
        /// <summary>
        /// Generate a jwt token bases on HmacSha256 algorithm.
        /// </summary>
        /// <param name="secret">Secret key for generating jwt token.</param>
        /// <param name="claims">List of parameters need to be sent over the jwt token.</param>
        /// <param name="expAfterHour">The jwt token will expire after a specific hour, by default it will expire after one hour.</param>
        /// <returns></returns>
        public static string GenerateToken(string secret, IList<Claim> claims, int? expAfterHour = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.UtcNow.AddHours(expAfterHour ?? 1),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
