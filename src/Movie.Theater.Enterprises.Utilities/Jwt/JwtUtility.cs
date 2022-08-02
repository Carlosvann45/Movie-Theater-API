using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Models.Misc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Movie.Theater.Enterprises.Utilities.Jwt
{
    public class JwtUtility : IJwtUtility
    {
        private readonly AppSettings appSettings;

        public JwtUtility(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public string GetEmailFromToken(string bearerToken)
        {
            string token = bearerToken[7..].Trim();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken? jwtSecurityToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtSecurityToken != null)
            {
                Claim? claim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

                if (claim != null) return claim.Value;
            }

            return "";
        }

        public bool ValidateToken(string bearerToken, string secret)
        {
            bool isValid;

            TokenValidationParameters validationParam = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret)),
                ValidIssuer = Constants.URL,
                ValidAudience = Constants.URL,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            try
            {
                string token = bearerToken[7..].Trim();

                handler.ValidateToken(token, validationParam, out SecurityToken validatedToken);

                isValid = true;

            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }
        /// <summary>
        /// Create A JWT Token base from a users username
        /// </summary>
        /// <param name="user">user to provide a username for</param>
        /// <returns>JWT token string</returns>
        public string CreateAccessToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettings.AccessSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: Constants.URL,
                audience: Constants.URL,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Create A JWT Token base from a users username
        /// </summary>
        /// <param name="user">user to provide a username for</param>
        /// <returns>JWT token string</returns>
        public string CreatRefresherToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettings.RefreshSecret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: Constants.URL,
                audience: Constants.URL,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
