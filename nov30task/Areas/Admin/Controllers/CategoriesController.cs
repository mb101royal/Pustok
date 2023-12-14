﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.ViewModels.CategoriesVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        PustokDbContext _db { get; }

        public CategoriesController(PustokDbContext db)
        {
            _db = db;
        }

        // Index

        public async Task<IActionResult> Index()
        {
            var CaregoriesFromDb = await _db.Categories.Select(c => new CategoryListVM { Id = c.Id, Name = c.Name }).ToListAsync();

            return View(CaregoriesFromDb);
        }

        // Create

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _db.Categories.AnyAsync(s => s.Name == vm.Name))
            {
                ModelState.AddModelError("Name", vm.Name + " adı artıq mövcuddur.");
                return View(vm);
            }
            await _db.Categories.AddAsync(new Models.Category { Name = vm.Name });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var data = await _db.Categories.FindAsync(id);

            if (data == null) return NotFound();

            var categoryUpdate = new CategoryUpdateVM { Name = data.Name };

            return View(categoryUpdate);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, CategoryUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var data = await _db.Categories.FindAsync(id);

            if (data == null) return NotFound();

            data.Name = vm.Name;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            
            var data = await _db.Categories.FindAsync(id);
            
            if (data == null) return NotFound();
            
            _db.Categories.Remove(data);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
