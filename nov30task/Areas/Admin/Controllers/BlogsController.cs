using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.BlogsVM;

namespace nov30task.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class BlogsController : Controller
    {

        PustokDbContext _db { get; }

        public BlogsController(PustokDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var blogsFromDb = await _db.Blogs.Select(blog => new BlogListItemVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Author = blog.Author,
                Description = blog.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }).ToListAsync();

            return View(blogsFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Blog blog = new Blog
            {
                Title = vm.Title,
                Description = vm.Description,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                Id = vm.AuthorId,

            };

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public IActionResult Update(int? id)
        {
            return View();
        }

        // Post
        [HttpPost]
        public IActionResult Update(int? id, BlogUpdateVM vm)
        {
            return View();
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var blog = await _db.Blogs.FindAsync(id);

            if (blog == null) return NotFound();

            _db.Blogs.Remove(blog);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
