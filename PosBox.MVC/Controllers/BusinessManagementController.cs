using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;
using PosBox.BLL.DTOs;
using PosBox.BLL.Services;
using PosBox.MVC.Authorization;
using System.Security.Claims;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Controllers
{
    //[AuthorizedRole(UserRole.Admin)]
    [Route("BusinessManagement")]
    public class BusinessManagementController : Controller
    {
        private BusinessService businessService;
        private AuditLogService auditLogService;
        private readonly DriveUploadService driveUploadService;

        public BusinessManagementController(BusinessService businessService, AuditLogService auditLogService, DriveUploadService driveUploadService)
        {
            this.businessService = businessService;
            this.auditLogService = auditLogService;
            this.driveUploadService = driveUploadService;
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId.HasValue)
            {
                var businesses = businessService.GetAll(currentUserId.Value)
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
                return View(businesses);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var model = new BusinessDTO
            {
                // Pre-populate required fields with default values
                BusinessStatus = UserStatus.Active,
                PreferredLanguage = Language.English,
                PreferredTheme = Theme.Light,
                Cash = 0,
                CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "Admin",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            
            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(BusinessDTO model, IFormFile? logoFile)
        {
            var currentUserId = GetCurrentUserId();
            model.CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "Admin";
            model.CreatedAt = DateTime.UtcNow;
            model.IsDeleted = false;
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value != null && x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                    );
                    
                return Json(new { success = false, message = "Form validation failed. Please check your inputs.", errors });
            }
         
            if (logoFile != null && logoFile.Length > 0)
            {
                try
                {
                    string? logoUrl = await LogoUpload(logoFile, model.Name);
                    if (logoUrl != null)
                    {
                        model.LogoImageUrl = logoUrl;
                    }
                    else
                    {
                        return Json(new { success = false, message = "Logo upload failed!" });
                    }
                }
                catch (Exception)
                {
                    return Json(new { success = false, message = "Error uploading logo." });
                }
            }
            
            try
            {
                string errorMessage = string.Empty;
                bool result = businessService.RegisterBusiness(model, out errorMessage);

                if (result)
                {
                    if (currentUserId.HasValue)
                    {
                        auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Created, $"Business: {model.Name} ");
                    }
                    return Json(new { success = true, message = "Business Created Successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to create Business. " + errorMessage });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error creating business. Please try again." });
            }
        }

        [HttpPost]
        [Route("Block")]
        public JsonResult Block([FromBody] List<int> businessIds)
        {
            var currentUserId = GetCurrentUserId();
            if (businessIds == null || businessIds.Count == 0)
                return Json(new { success = false, message = "No businessess selected." });
            var result = businessService.BlockBusinesses(businessIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Blocked, "Ids: " + string.Join(", ", businessIds));
                    return Json(new { success = true, message = "Businesses blocked successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to block Businesses." });
            }
        }


        [HttpPost]
        [Route("Unblock")]
        public JsonResult Unblock([FromBody] List<int> businessIds)
        {
            var currentUserId = GetCurrentUserId();
            if (businessIds == null || businessIds.Count == 0)
                return Json(new { success = false, message = "No Businesses selected." });
            var result = businessService.UnblockBusinesses(businessIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Unblocked, "Ids: " + string.Join(", ", businessIds));
                    return Json(new { success = true, message = "Businesses unblocked successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to unblock Businesses." });
            }
        }

        [HttpPost]
        [Route("Activate")]
        public JsonResult Activate([FromBody] List<int> businessIds)
        {
            var currentUserId = GetCurrentUserId();
            if (businessIds == null || businessIds.Count == 0)
                return Json(new { success = false, message = "No Businesses selected." });
            var result = businessService.ActivateBusinesses(businessIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Activated, "Ids: " + string.Join(", ", businessIds));
                    return Json(new { success = true, message = "Businesses Activated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to Activate Businesses." });
            }
        }
        [HttpPost]
        [Route("Deactivate")]
        public JsonResult Deactivate([FromBody] List<int> businessIds)
        {
            var currentUserId = GetCurrentUserId();
            if (businessIds == null || businessIds.Count == 0)
                return Json(new { success = false, message = "No Businesses selected." });
            var result = businessService.DeActivateBusinesses(businessIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Deactivated, "Ids: " + string.Join(", ", businessIds));
                    return Json(new { success = true, message = "Businesses Deactivated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current Business." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to Deactivate Businesses." });
            }
        }
        
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete([FromBody] List<int> businessIds)
        {
            var currentUserId = GetCurrentUserId();
            if (businessIds == null || businessIds.Count == 0)
                return Json(new { success = false, message = "No Businesses selected." });
                
            // Using the DeleteBusinesseses method (note the typo in the original method name)
            var result = businessService.DeleteBusinesseses(businessIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Deleted, "Ids: " + string.Join(", ", businessIds));
                    return Json(new { success = true, message = "Businesses deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to delete Businesses." });
            }
        }

        public async Task<string?> LogoUpload(IFormFile? logoFile, string BusName)
        {
                if (logoFile == null)
                    return null;
                    
                string extension = Path.GetExtension(logoFile.FileName).ToLowerInvariant();
                string tempFilePath = Path.Combine(Path.GetTempPath(), $"posbox_logo_{DateTime.Now.Ticks}{extension}");
                try
                {
                    // Save the file to the temporary location first
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await logoFile.CopyToAsync(stream);
                    }
                    
                    string fileName = $"logo_{BusName}{extension}";

                    // Upload to the "Logos" folder in Google Drive
                    var uploadResult = await driveUploadService.UploadImg(
                        tempFilePath,
                        fileName,
                        $"Logo for {BusName}",
                        "Logos"
                    );

                    if (uploadResult != null)
                    {
                        return uploadResult.ViewableLink;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (System.IO.File.Exists(tempFilePath))
                    {
                        System.IO.File.Delete(tempFilePath);
                    }
                }
            }
    }   
}


