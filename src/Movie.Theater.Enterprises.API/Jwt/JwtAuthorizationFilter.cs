using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Utilities.Jwt;

namespace Movie.Theater.Enterprises.API.Jwt
{
    /// <summary>
    /// jwt authorization filter to authorize customers to specific methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> roles;

        public JwtAuthorizationFilter(params Role[] roles)

        {
            this.roles = roles ?? new Role[] { };
        }

        /// <summary>
        /// Handles authorizing any class/method that has the authorization attribute
        /// </summary>
        /// <param name="context">context to check if customer is authorized</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<JwtAllowAnonymous>().Any();
            if (allowAnonymous)
                return;

            Customer? user = (Customer?)context.HttpContext.Items["Customer"];
            string? error = (string?)context.HttpContext.Items["Error"];

            if (error != null)
            {
                context.Result = new JsonResult(new
                {
                    TimeStamp = DateTime.Now,
                    ErrorMessage = error,
                    ErrorCode = Constants.UNAUTHORIZED_ERROR
                })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (user == null)
            {
                context.Result = new JsonResult(new
                {
                    TimeStamp = DateTime.Now,
                    ErrorMessage = Constants.BAD_TOKEN,
                    ErrorCode = Constants.UNAUTHORIZED_ERROR
                })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (roles.Any() && !roles.Contains(user.Role))
            {
                context.Result = new JsonResult(new
                {
                    TimeStamp = DateTime.Now,
                    ErrorMessage = "You dont have proper role for access.",
                    ErrorCode = Constants.UNAUTHORIZED_ERROR
                })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}