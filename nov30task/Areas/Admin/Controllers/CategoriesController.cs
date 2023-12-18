using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.CategoriesVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        PustokDbContext Db { get; }

        public CategoriesController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        public async Task<IActionResult> Index()
        {
<<<<<<< HEAD
            var CaregoriesFromDb = await Db.Categories.Select(category => new CategoryListVM
            {
                Id = category.Id,
                Name = category.Name
            }).ToListAsync();
=======
            var CaregoriesFromDb = await _db.Categories.Select(c => new CategoryListItemVM { Id = c.Id, Name = c.Name }).ToListAsync();
>>>>>>> 1c03025e5f05c7c77e8ab41bf5db0c598bebb33f

            return View(CaregoriesFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (await Db.Categories.AnyAsync(s => s.Name == vm.Name))
            {
                ModelState.AddModelError("Name", vm.Name + " adı artıq mövcuddur.");
                return View(vm);
            }

            Category categoryToCreate = new() {Name = vm.Name};

            await Db.Categories.AddAsync(categoryToCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var categoryFromDb = await Db.Categories.FindAsync(id);

            if (categoryFromDb == null) return NotFound();

            CategoryUpdateVM categoryToUpdate = new() { Name = categoryFromDb.Name };

            return View(categoryToUpdate);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var categoryFromDb = await Db.Categories.FindAsync(id);

            if (categoryFromDb == null) return NotFound();

            categoryFromDb.Name = vm.Name;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            
            var categoryToDelete = await Db.Categories.FindAsync(id);
            
            if (categoryToDelete == null) return NotFound();
            
            Db.Categories.Remove(categoryToDelete);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
