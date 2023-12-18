using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.AuthorsVM;
using nov30task.ViewModels.SlidersVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AuthorsController : Controller
    {
        PustokDbContext Db { get; }

        public AuthorsController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        public async Task<IActionResult> Index()
        {
            var authorsFromDb = await Db.Authors.Select(a => new AuthorListItemVM
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
            }).ToListAsync();

            return View(authorsFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateVM vm)
        {
            if (!ModelState.IsValid) return BadRequest();

            Author author = new()
            {
                Name = vm.Name,
                Surname = vm.Surname,
            };

            await Db.AddAsync(author);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var data = await Db.Authors.FindAsync(id);

            if (data == null) return NotFound();

            var authorUpdate = new AuthorUpdateVM
            {
                Name = data.Name,
                Surname = data.Surname,
            };

            return View(authorUpdate);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, AuthorUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var author = await Db.Authors.FindAsync(id);

            if (author == null) return NotFound();

            author.Name = vm.Name;
            author.Surname = vm.Surname;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var authorToDelete = await Db.Authors.FindAsync(id);

            if (authorToDelete == null) return NotFound();

            Db.Authors.Remove(authorToDelete);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
