using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PosBox.MVC.Middleware
{
    public class JwtCookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if JWTToken cookie exists
            if (context.Request.Cookies.TryGetValue("JWTToken", out string token))
            {
                // Add the JWT token to the Authorization header
                context.Request.Headers.Authorization = $"Bearer {token}";
            }

            await _next(context);
        }
    }

    // Extension method to make it easier to add the middleware to the pipeline
    public static class JwtCookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtCookieAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtCookieAuthenticationMiddleware>();
        }
    }
}