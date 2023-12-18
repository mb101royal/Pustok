using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.BlogsVM;
using nov30task.ViewModels.SlidersVM;
using NuGet.Protocol.Plugins;

namespace nov30task.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class BlogsController : Controller
    {

        PustokDbContext Db { get; }

        public BlogsController(PustokDbContext db)
        {
            Db = db;
        }

        public async Task<IActionResult> Index()
        {
            var blogsFromDb = await Db.Blogs.Select(blog => new BlogListItemVM
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
            ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVM vm)
        {
            ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Blog blogToCreate = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                AuthorId = vm.AuthorId,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
            };

            await Db.Blogs.AddAsync(blogToCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            
            ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");

            var blogFromDb = await Db.Blogs.FindAsync(id);

            if (blogFromDb == null) return NotFound();

            BlogUpdateVM blogToUpdate = new()
            {
                UpdatedAt = blogFromDb.UpdatedAt,
                CreatedAt = blogFromDb.UpdatedAt,
                Description = blogFromDb.Description,
                AuthorId = blogFromDb.AuthorId,
                Title = blogFromDb.Title,
            };

            return View(blogToUpdate);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");
                return View(vm);

            }

            var blogFromDb = await Db.Blogs.FindAsync(id);

            if (blogFromDb == null) return NotFound();

            blogFromDb.UpdatedAt = vm.UpdatedAt;
            blogFromDb.AuthorId = vm.AuthorId;
            blogFromDb.Description = vm.Description;
            blogFromDb.Title = vm.Title;
            blogFromDb.CreatedAt = vm.CreatedAt;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var blogFromDb = await Db.Blogs.FindAsync(id);

            if (blogFromDb == null) return NotFound();

            Db.Blogs.Remove(blogFromDb);

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
