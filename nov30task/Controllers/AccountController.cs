using Microsoft.AspNetCore.Mvc;

namespace nov30task.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult MyAccount()
        {
            return View();
        }

        /*public IActionResult Orders()
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

        public IActionResult AccountDetail()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }*/
    }
}
