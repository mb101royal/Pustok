using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nov30task.Helpers;

namespace nov30task.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator, Member")]
    public class AccountController : Controller
    {
        public IActionResult MyAccount()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        /*
        public IActionResult Orders()
        {
            return View();
        }
        
        public IActionResult Download()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        
        public IActionResult Logout()
        {
            return View();
        }*/
    }
}
