using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movie.Theater.Enterprises.Models.Entities;
using Movie.Theater.Enterprises.Models.Misc;
using Movie.Theater.Enterprises.Utilities.Jwt;

namespace Movie.Theater.Enterprises.API.Jwt
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> roles;

        public JwtAuthorizationFilter(params Role[] roles)

        {
            this.roles = roles ?? new Role[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<JwtAllowAnonymous>().Any();
            if (allowAnonymous)
                return;

            Customer? user = (Customer?)context.HttpContext.Items["Customer"];

            if (user == null)
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
/*

            bool isValid = false;
            string token = string.Empty;
            string error = string.Empty;
            var headers = filterContext.HttpContext.Request.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                token = headers.First(h => h.Key == "Authorization").Value;

                if (!token.StartsWith("Bearer "))
                {
                    filterContext.ModelState.AddModelError("Unauthorized", "Your token does not start with Bearer ");
                }
                else
                {
                    token = token[7..].Trim();

                    if (token.ValidateToken("")) isValid = true;
                    else
                    {
                        filterContext.ModelState.AddModelError("Unauthorized", Constants.BAD_TOKEN);
                    }
                }
            }
            else
            {

                filterContext.ModelState.AddModelError("Unauthorized", "You must provide a Bearer token for access.");
            }

            if (!isValid)
            {
                filterContext.Result = new UnauthorizedObjectResult(filterContext.ModelState);
            } 

 */