using Microsoft.Extensions.Options;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Providers.Interfaces;
using Movie.Theater.Enterprises.Utilities.Jwt;

namespace Movie.Theater.Enterprises.API.Jwt
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly AppSettings appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ICustomerProvider userProvider, IJwtUtility jwtUtils)
        {
            string token = string.Empty;
            var headers = context.Request.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                token = headers.First(h => h.Key == "Authorization").Value;

                if (token.StartsWith("Bearer "))
                {
                    token = token[7..].Trim();

                    if (jwtUtils.ValidateToken(token, appSettings.AccessSecret))
                    {
                        var userEmail = jwtUtils.GetEmailFromToken(token);

                        if (userEmail != null)
                        {
                            // attach customer to context on successful jwt validation
                            // context.Items["Customer"] = await userProvider.GetUserByEmail(userEmail);
                        }
                    }
                }
            }

            await next(context);
        }
    }
}
