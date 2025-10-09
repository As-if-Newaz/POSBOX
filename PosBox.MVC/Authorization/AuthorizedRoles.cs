using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PosBox.BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Authorization
{
    public class AuthorizedRoles : Attribute, IAuthorizationFilter
    {
        private readonly UserRole[] allowedRoles;

        public AuthorizedRoles(params UserRole[] roles)
        {
            allowedRoles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["JWTToken"];

            if (string.IsNullOrEmpty(token))
            {
                RedirectToLogin(context, "Please log in to continue.");
                return;
            }
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    RedirectToLogin(context, "Your session has expired. Please log in again.");
                    return;
                }

                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    RedirectToLogin(context, "Invalid session, Please log in again.");
                    return;
                }

                var userService = context.HttpContext.RequestServices.GetRequiredService<UserService>();

                var user = userService.GetById(int.Parse(userId));
                if (user == null || user.UserStatus.Equals(UserStatus.Blocked))
                {
                    RedirectToLogin(context, "Invalid Session, Please log in again");
                    return;
                }
                
                // Check if the user's role is in the allowed roles list
                if (!allowedRoles.Contains(user.UserRole))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", new { errorMessage = "Access Denied" });
                    return;
                }
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new(ClaimTypes.Role, user.UserRole.ToString()),
                    new("PreferredLanguage", user.PreferredLanguage.ToString()),
                    new("PreferredTheme", user.PreferredTheme.ToString())
                };

                var identity = new ClaimsIdentity(claims, "JWT");
                var principal = new ClaimsPrincipal(identity);
                context.HttpContext.User = principal;
            }
            catch
            {
                RedirectToLogin(context, "An error occurred. Please log in again.");
            }
        }

        private void RedirectToLogin(AuthorizationFilterContext context, string message)
        {
            context.HttpContext.Response.Cookies.Delete("JWTToken");
            context.HttpContext.Response.Cookies.Delete("PreferredTheme");
            context.HttpContext.Response.Cookies.Delete("PreferredLanguage");
            context.HttpContext.Response.Cookies.Delete("Theme");
            context.HttpContext.Response.Cookies.Delete("Language");
            var redirectResult = new RedirectToActionResult("login", "Auth", new { errorMessage = message });
            context.Result = redirectResult;
        }
    }
}