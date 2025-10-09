using Microsoft.AspNetCore.Mvc;
using PosBox.BLL.Services;
using PosBox.MVC.Authorization;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.MVC.Controllers
{
    [AuthorizedRole(UserRole.Admin)]
    public class AdminController : Controller
    {

        public AdminController()
        {
        }    

        [HttpGet]
        public IActionResult Dashboard()
        { 
            
            return View();
        }
    }
}
