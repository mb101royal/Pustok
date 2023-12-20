using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;

namespace nov30task.Controllers
{
    public class HomeController : Controller
    {
        PustokDbContext Db { get; }

        public HomeController(PustokDbContext db)
        {
            Db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await Db.Sliders.ToListAsync();
            return View(sliders);
        }

        /*public string GetSession(string key)
        {
            return HttpContext.Session.GetString(key) ?? "";
            //HttpContext.Session.Remove(key);
        }

        public void GetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        public string GetCookie(string key)
        {
            return HttpContext.Request.Cookies[key] ?? "";
        }

        public void SetCookie(string key, string value)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                // MaxAge = TimeSpan.FromSeconds(100)
                // Expires = DateTime.UtcNow.AddDays(10)
            }) ;
            // HttpContext.Response.Cookies.Delete(key);
        }*/


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
