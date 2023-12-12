using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.SlidersVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        PustokDbContext _db { get; }

        public SlidersController(PustokDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            var sliderFromDb = await _db.Sliders.Select(slider => new SliderListItemVM
            {
                Id = slider.Id,
                Title = slider.Title,
                ButtonText = slider.ButtonText,
                ImageUrl = slider.ImageUrl,
                Position = slider.Position,
                Text = slider.Text
            }).ToListAsync();

            return View(sliderFromDb);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (vm.Position == null)
            {
                ModelState.AddModelError("Position", "Xeta");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Slider slider = new Slider
            {
                Title = vm.Title,
                Text = vm.Text,
                ImageUrl = vm.ImageUrl,
                Position = vm.Position switch
                {
                    0 => false,
                    1 => true
                },
                ButtonText = vm.ButtonText,
            };

            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            TempData["SliderDeletionResponse"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            TempData["SliderDeletionResponse"] = true;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            return View(new SliderUpdateVM
            {
                ImageUrl = data.ImageUrl,
                Position = data.Position switch
                {
                    false => 0,
                    true => 1
                },
                Text = data.Text,
                Title = data.Title,
                ButtonText = data.ButtonText
            });
        }
        [HttpPost]

        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            TempData["SliderRenovationResponse"] = false;

            if (id == null || id <= 0) return BadRequest();
            if (vm.Position < -1 || vm.Position > 1)
            {
                ModelState.AddModelError("Position", "Xeta");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            data.Text = vm.Text;
            data.Title = vm.Title;
            data.ImageUrl = vm.ImageUrl;
            data.Position = vm.Position switch
            {
                0 => false,
                1 => true
            };
            data.ButtonText = vm.ButtonText;
            TempData["SliderRenovationResponse"] = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
