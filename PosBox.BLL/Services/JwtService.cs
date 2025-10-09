using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PosBox.BLL.Config;
using PosBox.BLL.DTOs;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.Services
{
    public class JwtService
    {
        // Configuration is retained for backward compatibility and other settings
        private readonly IConfiguration cfg;
        public JwtService(IConfiguration configuration)
        {
            this.cfg = configuration;
        }
        public string GenerateUserToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretManager.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.UserRole.ToString()),
                new("PreferredLanguage", user.PreferredLanguage.ToString()),
                new("PreferredTheme", user.PreferredTheme.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: SecretManager.JwtIssuer,
                audience: SecretManager.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string GenerateBusinessToken(BusinessDTO business)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretManager.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, business.Id.ToString()),
                new Claim(ClaimTypes.Name, business.BusinessUserName),
                new Claim(ClaimTypes.Role, UserRole.Business.ToString()),
                new("PreferredLanguage", business.PreferredLanguage.ToString()),
                new("PreferredTheme", business.PreferredTheme.ToString())
            };
            var token = new JwtSecurityToken(
                issuer: SecretManager.JwtIssuer,
                audience: SecretManager.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
