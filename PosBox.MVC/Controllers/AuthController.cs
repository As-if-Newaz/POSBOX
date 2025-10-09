using Microsoft.AspNetCore.Mvc;
using PosBox.BLL.DTOs;
using PosBox.BLL.Services;
using PosBox.DAL.Entity_Framework.Table_Models;
using PosBox.MVC.Models;
using System.Diagnostics;
using System.Security.Claims;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;
        private readonly JwtService jwtService;
        private readonly AuditLogService auditLogService;

        public AuthController(AuthService authService, JwtService jwtService, AuditLogService auditLogService)
        {
            this.authService = authService;
            this.jwtService = jwtService;
            this.auditLogService = auditLogService;
        }

        [HttpGet]
        public IActionResult Login(string errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData["ErrorMsg"] = errorMessage;
            }
            
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO, bool rememberMe = false)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            string errorMsg;
            var user = authService.AuthenticateUser(loginDTO, out errorMsg);
            // We'll skip business authentication for now since we only want to login as a user
            if (user == null)
            {
                TempData["ErrorMsg"] = "Login Failed! Invalid username or password.";
                return View(loginDTO);
            }
            if (user != null)
            {
                try
                {
                    SetAuthCookies(user, rememberMe);

                    // Log successful login
                    auditLogService.RecordLog(user.Id, user.UserRole, AuditActions.LoggedIn, $"{user.UserName} logged in successfully");

                    var statusResult = HandleUserStatus(user, loginDTO);
                    if (statusResult != null)
                    {
                        return statusResult;
                    }

                    return GetRoleBasedRedirect(user.UserRole);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMsg"] = "An error occurred during login. Please try again.";
                    auditLogService.RecordLog(user?.Id ?? 0, user?.UserRole ?? 0, AuditActions.LoginAttempt, $"Login error: {ex.Message}");
                    return View(loginDTO);
                }
            }
            return View(loginDTO);
        }

        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {

            var IdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Role = User.FindFirst(ClaimTypes.Role)?.Value;
            int Id = int.TryParse(IdClaim, out var id) ? id : 0;
            if (Enum.TryParse(typeof(UserRole), Role, out var parsedRole) && parsedRole is UserRole userRole)
            {
                auditLogService.RecordLog(Id, userRole, AuditActions.LoggedOut, "logged out successfully");
            }
            else
            {
                auditLogService.RecordLog(Id, UserRole.Unknown, AuditActions.LoggedOut, "logged out successfully");
            }

            ClearAuthCookies();

            TempData["SuccessMsg"] = "Logged out successfully!";
            return RedirectToAction("login", "Auth");
        }

        private void ClearAuthCookies()
        {
            HttpContext.Response.Cookies.Delete("JWTToken");
            HttpContext.Response.Cookies.Delete("Theme");
            HttpContext.Response.Cookies.Delete("Language");
        }

        private void SetAuthCookies(UserDTO user, bool rememberMe)
        {
            var token = jwtService.GenerateUserToken(user);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(3)
            };
            HttpContext.Response.Cookies.Append("JWTToken", token, cookieOptions);
            var clientCookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddHours(3)
            };
            HttpContext.Response.Cookies.Append("Theme", user.PreferredTheme.ToString(), clientCookieOptions);
            HttpContext.Response.Cookies.Append("Language", user.PreferredLanguage.ToString(), clientCookieOptions);
        }

        private IActionResult? HandleUserStatus(UserDTO user, LoginDTO loginDTO)
        {
            if (user.UserStatus == UserStatus.Inactive)
            {
                TempData["ErrorMsg"] = "Your account is inactive. Please verify your email.";
                ClearAuthCookies();
                return RedirectToAction("VerifyEmail", "User");
            }
            else if (user.UserStatus == UserStatus.Blocked)
            {
                TempData["ErrorMsg"] = "Your account is blocked. Please contact support.";
                ClearAuthCookies();
                return View(loginDTO);
            }
            return null;
        }

        private IActionResult GetRoleBasedRedirect(UserRole role)
        {
            if (role == UserRole.Admin)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (role == UserRole.Manager)
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            // Default redirect for other roles (e.g., User)
            return RedirectToAction("Index", "Home");
        }
    }
}
