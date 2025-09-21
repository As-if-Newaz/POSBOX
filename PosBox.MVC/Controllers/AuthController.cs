using Microsoft.AspNetCore.Mvc;
using PosBox.BLL.DTOs;
using PosBox.BLL.Services;
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
        private readonly UserService userServices;
        private readonly AuditLogService auditLogService;

        public AuthController(AuthService authService, JwtService jwtService, UserService userServices, AuditLogService auditLogService)
        {
            this.authService = authService;
            this.jwtService = jwtService;
            this.userServices = userServices;
            this.auditLogService = auditLogService;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                TempData["ErrorMsg"] = errorMessage;
            }
            return View(new LoginDTO());
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            string errorMsg;
            var user = authService.AuthenticateUser(loginDTO, out errorMsg);
            var business = authService.AuthenticateBusiness(loginDTO, out errorMsg);
            if (user == null && business ==  null)
            {
                TempData["ErrorMsg"] = errorMsg;
                return View(loginDTO);
            }
            if (user != null)
            {
                try
                {
                    // Generate JWT
                    var token = jwtService.GenerateUserToken(user);

                    // Store JWT in a secure cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.Now.AddHours(3)
                    };
                    HttpContext.Response.Cookies.Append("JWTToken", token, cookieOptions);

                    // Store non-sensitive data for client-side use (optional)
                    var clientCookieOptions = new CookieOptions
                    {
                        HttpOnly = false, // Allow client-side access
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.Now.AddHours(3)
                    };
                    HttpContext.Response.Cookies.Append("Theme", user.PreferredTheme.ToString(), clientCookieOptions);
                    HttpContext.Response.Cookies.Append("Language", user.PreferredLanguage.ToString(), clientCookieOptions);

                    // Log successful login
                    auditLogService.RecordLog(user.Id, user.UserRole, AuditActions.LoggedIn, $"{user.UserName} logged in successfully");

                    // Check user status
                    if (user.UserStatus == UserStatus.Inactive)
                    {
                        TempData["ErrorMsg"] = "Your account is inactive. Please verify your email.";
                        // Clear cookies on error
                        HttpContext.Response.Cookies.Delete("JWTToken");
                        HttpContext.Response.Cookies.Delete("Theme");
                        HttpContext.Response.Cookies.Delete("Language");
                        return RedirectToAction("VerifyEmail", "User");
                    }
                    else if (user.UserStatus == UserStatus.Blocked)
                    {
                        TempData["ErrorMsg"] = "Your account is blocked. Please contact support.";
                        // Clear cookies on error
                        HttpContext.Response.Cookies.Delete("JWTToken");
                        HttpContext.Response.Cookies.Delete("Theme");
                        HttpContext.Response.Cookies.Delete("Language");
                        return View(loginDTO);
                    }

                    // Redirect based on role
                    if (user.UserRole == UserRole.Admin)
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (user.UserRole == UserRole.Manager)
                    {
                        return RedirectToAction("Dashboard", "Manager");
                    }

                    // Default redirect for other roles (e.g., User)
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMsg"] = "An error occurred during login. Please try again.";
                    auditLogService.RecordLog(user?.Id ?? 0, user?.UserRole ?? 0, AuditActions.LoginAttempt, $"Login error: {ex.Message}");
                    return View(loginDTO);
                }
            }
            if(business !=  null)
            {
                try
                {
                    // Generate JWT
                    var token = jwtService.GenerateBusinessToken(business);
                    // Store JWT in a secure cookie
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.Now.AddHours(3)
                    };
                    HttpContext.Response.Cookies.Append("JWTToken", token, cookieOptions);
                    // Store non-sensitive data for client-side use (optional)
                    var clientCookieOptions = new CookieOptions
                    {
                        HttpOnly = false, // Allow client-side access
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.Now.AddHours(3)
                    };
                    HttpContext.Response.Cookies.Append("Theme", business.PreferredTheme.ToString(), clientCookieOptions);
                    HttpContext.Response.Cookies.Append("Language", business.PreferredLanguage.ToString(), clientCookieOptions);
                    // Log successful login
                    auditLogService.RecordLog(business.Id, UserRole.Business, AuditActions.LoggedIn, $"{business.Name} logged in successfully");
                    // Redirect to business dashboard
                    return RedirectToAction("Dashboard", "Business");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMsg"] = "An error occurred during login. Please try again.";
                    auditLogService.RecordLog(business?.Id ?? 0, UserRole.Business, AuditActions.LoginAttempt, $"Login error: {ex.Message}");
                    return View(loginDTO);
                }
            }
            TempData["ErrorMsg"] = "Some other login complication";
            return View(loginDTO);
        }

        //[Authenticated]
        [Route("logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            // Safely retrieve user ID from JWT claims instead of cookie
            var IdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Role = User.FindFirst(ClaimTypes.Role)?.Value;
            int Id = int.TryParse(IdClaim, out var id) ? id : 0;

            // Log logout action
            // Replace this line in the Logout method:
            // auditLogService.RecordLog(Id, Role, AuditActions.LoggedOut, "User logged out successfully");

            // With the following code to parse the string Role to Enums.UserRole:
            if (Enum.TryParse(typeof(UserRole), Role, out var parsedRole) && parsedRole is UserRole userRole)
            {
                auditLogService.RecordLog(Id, userRole, AuditActions.LoggedOut, "logged out successfully");
            }
            else
            {
                // Fallback if role is not valid, use a default or handle as needed
                auditLogService.RecordLog(Id, UserRole.Unknown, AuditActions.LoggedOut, "logged out successfully");
            }

            // Clear cookies
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1) // Expire immediately
            };
            HttpContext.Response.Cookies.Append("JWTToken", "", cookieOptions);
            HttpContext.Response.Cookies.Append("Theme", "", cookieOptions);
            HttpContext.Response.Cookies.Append("Language", "", cookieOptions);

            TempData["SuccessMsg"] = "Logged out successfully!";
            return RedirectToAction("login", "Auth");
        }
    }
}
