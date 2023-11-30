using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using System.Diagnostics;

namespace nov30task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public async Task<IActionResult> Index()
        {
            using ApplicationDbContext context = new ApplicationDbContext();
            var sliders = await context.Sliders.ToListAsync();

            return View(sliders);
        }
    }
}