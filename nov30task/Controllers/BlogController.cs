using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;

namespace nov30task.Controllers
{
    public class BlogController : Controller
    {
        PustokDbContext Db { get; }

        public BlogController(PustokDbContext db)
        {
            Db = db;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await Db.Blogs.ToListAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 0) return BadRequest();

            var detail = await Db.Blogs.FindAsync(id);

            return View(detail);
        }
    }
}
