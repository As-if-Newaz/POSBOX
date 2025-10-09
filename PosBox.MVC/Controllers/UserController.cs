using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PosBox.BLL.DTOs;
using PosBox.BLL.Services;
using PosBox.MVC.Authorization;
using PosBox.MVC.Models;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Controllers
{
    public class UserController : Controller
    {
        private UserService userServices;
        private EmailService emailService;
        public UserController(UserService userServices, EmailService emailService)
        {
            this.userServices = userServices;
            this.emailService = emailService;
        }
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserDTO());
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO, string confirmPassword)
        {
            // Debug: Log the received DTO properties
            Console.WriteLine($"Register POST received: Username={userDTO.UserName}, Email={userDTO.Email}, Role={userDTO.UserRole}");
            
            if (userDTO.Password != confirmPassword)
            {
                TempData["ErrorMsg"] = "Passwords do not match.";
                return View(userDTO);
            }
            
            // Set default values explicitly to ensure they're not null
            if (userDTO.UserRole == 0) // If it's the default value
            {
                userDTO.UserRole = UserRole.Manager; // Set to Manager explicitly
            }
            
            if (userDTO.PreferredLanguage == null)
            {
                userDTO.PreferredLanguage = Language.English;
            }
            
            if (userDTO.PreferredTheme == null)
            {
                userDTO.PreferredTheme = Theme.Light;
            }
            
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Model is valid, proceeding with registration for {userDTO.Email}");
                
                string errorMsg = string.Empty;
                var result = userServices.Register(userDTO, out errorMsg);
                
                if (result)
                {
                    Console.WriteLine($"Registration successful for {userDTO.Email}, verification code: {userDTO.EmailVerificationCode}");
                    
                    string subject = "Verification Code for PosBox";
                    string body = $"Your verification code for PosBox registration is: {userDTO.EmailVerificationCode}";
                    
                    try
                    {
                        await emailService.SendEmailAsync(userDTO.Email, subject, body);
                        Console.WriteLine($"Email sent successfully to {userDTO.Email}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email: {ex.Message}");
                        // Continue even if email fails
                    }
                    
                    TempData["SuccessMsg"] = "Registered Successfully! Please check your email for the verification code.";
                    return RedirectToAction("VerifyEmail", "User", new { email = userDTO.Email });
                }
                else
                {
                    Console.WriteLine($"Registration failed: {errorMsg}");
                    TempData["ErrorMsg"] = errorMsg;
                    return View(userDTO);
                }
            }
            else
            {
                Console.WriteLine("Model validation failed:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }
            
            return View(userDTO);
        }

        [HttpGet]
        [Route("verify-email")]
        public IActionResult VerifyEmail(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        [Route("verify-email")]
        public IActionResult VerifyEmail(string email, string code)
        {
            var user = userServices.GetUserByEmail(email);
            if (user == null)
            {
                TempData["ErrorMsg"] = "User not found.";
                return View();
            }
            if (user.EmailVerificationCode != code)
            {
                TempData["ErrorMsg"] = "Invalid verification code.";
                ViewBag.Email = email;
                return View();
            }
            if (user.EmailVerificationExpiry < DateTime.UtcNow)
            {
                TempData["ErrorMsg"] = "Verification code expired.";
                ViewBag.Email = email;
                return View();
            }
            user.UserStatus = UserStatus.Active;
            user.EmailVerificationCode = null;
            user.EmailVerificationExpiry = null;
            userServices.UpdateUser(user);
            TempData["SuccessMsg"] = "Email verified! You can now log in.";
            return RedirectToAction("Login", "Auth");
        }

        [AuthorizedRole(UserRole.Admin)]
        [HttpPost]
        public IActionResult UpdateThemePreference([FromBody] ThemePreference model)
        {
            if (model == null || model.Id <= 0)
                return Json(new { success = false, message = "Invalid data" });
            var user = userServices.GetById(model.Id);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            var theme = model.Theme == "Dark" ? Theme.Dark : Theme.Light;
            var preferences = new UserPreferencesDTO
            {
                UserId = model.Id,
                PreferredLanguage = user.PreferredLanguage ?? Language.English,
                PreferredTheme = theme
            };

            var result = userServices.UpdatePreferences(model.Id, preferences);
            return Json(new { success = result });
        }

        [AuthorizedRole(UserRole.Admin)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = HttpContext.Request.Cookies["Role"];
            var loggedId = HttpContext.Request.Cookies["LoggedId"];
            if (role != "Admin" && loggedId != null && id.ToString() != loggedId)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = userServices.GetById(id);
            var data = GetMapper().Map<UserDTO>(user);
            return View(data);
        }
        [AuthorizedRole(UserRole.Admin)]
        [HttpPost]
        public JsonResult Edit([FromBody] UserDTO obj)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(obj.Id);
                if (userServices.GetById(obj.Id) != null)
                {
                    var data = userServices.UpdateUser(obj);
                    return Json(new { success = true, message = "User updated successfully." });
                }
                return Json(new { success = false, message = "Invalid User" });
            }

            return Json(new { success = false, message = "Invalid User Data" });
        }
    }
}
