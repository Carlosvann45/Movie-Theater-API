using Microsoft.Extensions.Options;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Providers.Interfaces;
using Movie.Theater.Enterprises.Utilities.Jwt;

namespace Movie.Theater.Enterprises.API.Jwt
{
    /// <summary>
    /// Jwt middleware file to grab authorization tokens
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly AppSettings appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles grabbing the jwt tokens on request sent into the api
        /// </summary>
        /// <param name="context">context to get tokens from</param>
        /// <param name="customerProvider">customer provider to get customer information</param>
        /// <param name="jwtUtils">jwt utillity file for token</param>
        /// <returns>task</returns>
        public async Task Invoke(HttpContext context, ICustomerProvider customerProvider, IJwtUtility jwtUtils)
        {
            string token = string.Empty;
            var headers = context.Request.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                token = headers.First(h => h.Key == "Authorization").Value;

                if (token.StartsWith("Bearer "))
                {
                    if (jwtUtils.ValidateToken(token, appSettings.AccessSecret))
                    {
                        var customerEmail = jwtUtils.GetEmailFromToken(token);

                        if (customerEmail != null)
                        {
                            // attach customer to context on successful jwt validation
                            context.Items["Customer"] = await customerProvider.GetCustomerByEmailAsync(customerEmail);
                        }
                        else context.Items["Error"] = Constants.BAD_TOKEN;
                    }
                    else context.Items["Error"] = Constants.BAD_TOKEN;
                }
                else context.Items["Error"] = Constants.NO_BEARER_TOKEN;
            }
            else context.Items["Error"] = Constants.NO_TOKEN;

            await next(context);
        }
    }
}
