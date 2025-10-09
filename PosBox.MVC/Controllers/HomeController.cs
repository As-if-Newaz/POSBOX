using Microsoft.AspNetCore.Mvc;

namespace PosBox.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect to login page
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}