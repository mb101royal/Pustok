using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.AuthorsVM;
using nov30task.ViewModels.SlidersVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    /*[Authorize(Roles = "SuperAdmin, Admin, Moderator")]*/
    public class AuthorsController : Controller
    {
        PustokDbContext Db { get; }

        public AuthorsController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var authorsFromDb = await Db.Authors.Select(authorFromDb => new AuthorListItemVM
            {
                Id = authorFromDb.Id,
                Name = authorFromDb.Name,
                Surname = authorFromDb.Surname,
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
        public async Task<IActionResult> Create(AuthorCreateVM authorCreateViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            Author authorCreate = new()
            {
                Name = authorCreateViewModel.Name,
                Surname = authorCreateViewModel.Surname,
            };

            await Db.AddAsync(authorCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var authorFromDb = await Db.Authors.FindAsync(id);

            if (authorFromDb == null) return NotFound();

            AuthorUpdateVM authorUpdateViewModel = new()
            {
                Name = authorFromDb.Name,
                Surname = authorFromDb.Surname,
            };

            return View(authorUpdateViewModel);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, AuthorUpdateVM authorUpdateViewModel)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(authorUpdateViewModel);

            var authorFromDb = await Db.Authors.FindAsync(id);

            if (authorFromDb == null) return NotFound();

            authorFromDb.Name = authorUpdateViewModel.Name;
            authorFromDb.Surname = authorUpdateViewModel.Surname;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var authorFromDb = await Db.Authors.FindAsync(id);

            if (authorFromDb == null) return NotFound();

            Db.Authors.Remove(authorFromDb);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
