using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.CategoriesVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "SuperAdmin, Admin, Moderator")]
	public class CategoriesController : Controller
    {
        PustokDbContext Db { get; }

        public CategoriesController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        [AllowAnonymous]
        public async Task<IActionResult> Index()
		{
            var CaregoriesFromDb = await Db.Categories.Select(categoryFromDb => new CategoryListItemVM
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name
            }).ToListAsync();

            return View(CaregoriesFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            ViewBag.ParentCategories = new SelectList(Db.Categories, "Id", "Name");
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM categoryCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentCategories = new SelectList(Db.Categories, "Id", "Name");
                return View(categoryCreateViewModel);
            }

            if (await Db.Categories.AnyAsync(s => s.Name == categoryCreateViewModel.Name))
            {
                ModelState.AddModelError("Name", categoryCreateViewModel.Name + " adı artıq mövcuddur.");
                return View(categoryCreateViewModel);
            }

            Category categoryCreate = new()
            {
                Name = categoryCreateViewModel.Name,
                ParentCategoryId = (categoryCreateViewModel.ParentCategoryId != 0) ? categoryCreateViewModel.ParentCategoryId : 1
            };

            await Db.Categories.AddAsync(categoryCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            
            ViewBag.ParentCategories = new SelectList(Db.Categories, "Id", "Name");

            var categoryFromDb = await Db.Categories.FindAsync(id);

            if (categoryFromDb == null) return NotFound();

            CategoryUpdateVM categoryUpdateViewModel = new()
            {
                Name = categoryFromDb.Name,
                ParentCategoryId = (categoryFromDb.ParentCategoryId != 0) ? categoryFromDb.ParentCategoryId : 1
            };

            return View(categoryUpdateViewModel);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM categoryUpdateViewModel)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(categoryUpdateViewModel);

            var categoryFromDb = await Db.Categories.FindAsync(id);

            if (categoryFromDb == null) return NotFound();

            categoryFromDb.Name = categoryUpdateViewModel.Name;
            categoryFromDb.ParentCategoryId = (categoryUpdateViewModel.ParentCategoryId != 0) ? categoryUpdateViewModel.ParentCategoryId : 1;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            
            var categoryFromDb = await Db.Categories.FindAsync(id);
            
            if (categoryFromDb == null) return NotFound();
            
            Db.Categories.Remove(categoryFromDb);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
