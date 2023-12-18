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

        // Index:

        public async Task<IActionResult> Index()
        {
            var blogsFromDb = await Db.Blogs.Select(blogFromDb => new BlogListItemVM
            {
                Id = blogFromDb.Id,
                Title = blogFromDb.Title,
                Author = blogFromDb.Author,
                Description = blogFromDb.Description,
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
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");
                return View(vm);
            }

            Blog blogCreate = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                AuthorId = vm.AuthorId >= 1 ? vm.AuthorId : null,
            };

            await Db.Blogs.AddAsync(blogCreate);
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

            BlogUpdateVM blogUpdateViewModel = new()
            {
                Description = blogFromDb.Description,
                AuthorId = blogFromDb.AuthorId,
                Title = blogFromDb.Title,
            };

            return View(blogUpdateViewModel);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, BlogUpdateVM blogUpdateViewModel)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(Db.Authors, "Id", "Name");
                return View(blogUpdateViewModel);
            }

            /*var author = await Db.Authors.FindAsync(blogUpdateViewModel.AuthorId);*/

            var blogFromDb = await Db.Blogs.FindAsync(id);

            if (blogFromDb == null) return NotFound();

            blogFromDb.AuthorId = blogUpdateViewModel.AuthorId;
            blogFromDb.Description = blogUpdateViewModel.Description;
            blogFromDb.Title = blogUpdateViewModel.Title;


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
