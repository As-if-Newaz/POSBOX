using Microsoft.AspNetCore.Mvc;
using PosBox.BLL.Services;
using PosBox.MVC.Authorization;
using System.Security.Claims;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Controllers
{
    [AuthorizedRole(UserRole.Admin)]
    public class UserManagementController : Controller
    {
        private UserService userServices;
        private AuditLogService auditLogService;
        public UserManagementController(UserService userServices, AuditLogService auditLogService)
        {
            this.userServices = userServices;
            this.auditLogService = auditLogService;
        }
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var users = userServices.GetAll()
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            return View(users);
        }

        [HttpPost("UserManagement/Block")]
        public JsonResult Block([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.BlockUsers(userIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Blocked, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Users blocked successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to block users." });
            }
        }


        [HttpPost("UserManagement/Unblock")]
        public JsonResult Unblock([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.UnblockUsers(userIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Unblocked, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Users unblocked successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to unblock users." });
            }
        }

        [HttpPost("UserManagement/Activate")]
        public JsonResult Activate([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.ActivateUsers(userIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Activated, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Users Activated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to Activate users." });
            }
        }
        [HttpPost("UserManagement/Deactivate")]
        public JsonResult Deactivate([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.InactivateUsers(userIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Deactivated, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Users Deactivated successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to Deactivate users." });
            }
        }
        [HttpPost("UserManagement/AddAdminAccess")]
        public JsonResult GiveAdminAccess([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.UpdateUserRole(userIds, UserRole.Admin);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.GaveAccess, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Admin access granted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to grant admin access." });
            }
        }

        [HttpPost("UserManagement/RemoveAdminAccess")]
        public JsonResult RemoveAdminAccess([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.UpdateUserRole(userIds, UserRole.Manager);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.RemovedAccess, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Admin access removed successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to remove admin access." });
            }
        }

        [HttpPost("UserManagement/Delete")]
        public JsonResult Delete([FromBody] List<int> userIds)
        {
            var currentUserId = GetCurrentUserId();
            if (userIds == null || userIds.Count == 0)
                return Json(new { success = false, message = "No users selected." });
            var result = userServices.DeleteUsers(userIds);
            if (result)
            {
                if (currentUserId.HasValue)
                {
                    auditLogService.RecordLog(currentUserId.Value, UserRole.Admin, AuditActions.Deleted, "Ids: " + string.Join(", ", userIds));
                    return Json(new { success = true, message = "Users deleted successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to retrieve current user." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Failed to delete users." });
            }
        }
    }
}
