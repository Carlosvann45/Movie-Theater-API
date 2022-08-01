using Microsoft.IdentityModel.Tokens;
using Movie.Theater.Enterprises.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Movie.Theater.Enterprises.Utilities.Jwt
{
    public static class JwtUtility
    {
        /// <summary>
        /// Create A JWT Token base from a users username
        /// </summary>
        /// <param name="user">user to provide a username for</param>
        /// <returns>JWT token string</returns>
        public static string CreateAccessToken(this Customer customer, string secret)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(4),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Create A JWT Token base from a users username
        /// </summary>
        /// <param name="user">user to provide a username for</param>
        /// <returns>JWT token string</returns>
        public static string CreatRefresherToken(this Customer customer, string secret)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
