using Microsoft.AspNetCore.Mvc;

namespace nov30task.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public  IActionResult Create()
        {

        }
    }
}
