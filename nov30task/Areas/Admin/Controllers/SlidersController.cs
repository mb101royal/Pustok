﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.SlidersVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        PustokDbContext Db { get; }

        public SlidersController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        public async Task<IActionResult> Index()
        {

            var slidersFromDb = await Db.Sliders.Select(slider => new SliderListItemVM
            {
                Id = slider.Id,
                Title = slider.Title,
                ButtonText = slider.ButtonText,
                ImageUrl = slider.ImageUrl,
                Position = slider.Position,
                Text = slider.Text
            }).ToListAsync();

            return View(slidersFromDb);
        }

        // Create:
        
        // Get
        public IActionResult Create()
        {
            return View();
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.Position > 1 || vm.Position < 0) ModelState.AddModelError("Position", "Xeta");

            if (!ModelState.IsValid) return View(vm);

            Slider sliderToCreate = new()
            {
                Title = vm.Title,
                Text = vm.Text,
                ImageUrl = vm.ImageUrl,
                Position = vm.Position switch
                {
                    0 => false,
                    1 => true,
                    _ => throw new NotImplementedException(),
                },
                ButtonText = vm.ButtonText,
            };

            await Db.Sliders.AddAsync(sliderToCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var sliderFromDb = await Db.Sliders.FindAsync(id);

            if (sliderFromDb == null) return NotFound();

            SliderUpdateVM sliderToUpdate = new()
            {
                ImageUrl = sliderFromDb.ImageUrl,
                Position = sliderFromDb.Position switch
                {
                    false => 0,
                    true => 1
                },
                Text = sliderFromDb.Text,
                Title = sliderFromDb.Title,
                ButtonText = sliderFromDb.ButtonText
            };

            return View(sliderToUpdate);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {

            if (id == null || id <= 0) return BadRequest();
<<<<<<< HEAD
=======

            if (vm.Position < -1 || vm.Position > 1) ModelState.AddModelError("Position", "Xeta");

            if (!ModelState.IsValid) return View(vm);
>>>>>>> 1c03025e5f05c7c77e8ab41bf5db0c598bebb33f

            if (vm.Position < 0 || vm.Position > 1) ModelState.AddModelError("Position", "Xeta");

            if (!ModelState.IsValid) return View(vm);

            var sliderFromDb = await Db.Sliders.FindAsync(id);

            if (sliderFromDb == null) return NotFound();

            sliderFromDb.Text = vm.Text;
            sliderFromDb.Title = vm.Title;
            sliderFromDb.ImageUrl = vm.ImageUrl;
            sliderFromDb.ButtonText = vm.ButtonText;
            sliderFromDb.Position = vm.Position switch
            {
                0 => false,
                1 => true,
                _ => throw new NotImplementedException()
            };

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete:

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var sliderToDelete = await Db.Sliders.FindAsync(id);

            if (sliderToDelete == null) return NotFound();

            Db.Sliders.Remove(sliderToDelete);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
