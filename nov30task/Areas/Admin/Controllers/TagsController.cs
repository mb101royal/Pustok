using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.SlidersVM;
using nov30task.ViewModels.TagsVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {

        PustokDbContext _db { get; }

        public TagsController(PustokDbContext db)
        {
            _db = db;
        }

        // Index

        public IActionResult Index()
        {

            var tagsFromDb = _db.Tags.Select(tag => new TagListItemVM
            {
                Id = tag.Id,
                Name = tag.Name
            }).ToList();

            return View(tagsFromDb);
        }

        // Create:

        // Get
        public IActionResult Create()
        {
            return View();
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Create(TagCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var nameInDb = await _db.Tags.FirstOrDefaultAsync(n=> n.Name == vm.Name);
            if (nameInDb != null)
            {
                if (nameInDb.Name != vm.Name)
                {
                    Tag tagCreate = new Tag { Name = vm.Name };

                    await _db.Tags.AddAsync(tagCreate);
                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Name is repeated");
                    return View(vm);
                }
            }
            else
            {
                Tag tagCreate = new Tag { Name = vm.Name };

                await _db.Tags.AddAsync(tagCreate);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var tag = await _db.Tags.FindAsync(id);

            if (tag == null) return NotFound();

            var tagUpdate = new TagUpdateVM { Name = tag.Name };

            return View(tagUpdate);
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, TagUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var tag = await _db.Tags.FindAsync(id);
            
            if (tag == null) return NotFound();

            var nameInDb = await _db.Tags.FirstOrDefaultAsync(n => n.Name == vm.Name);
            if (nameInDb != null)
            {
                if (nameInDb.Name != vm.Name)
                {
                    tag.Name = vm.Name;

                    await _db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Name is repeated");
                    return View(vm);
                }
            }
            else
            {
                tag.Name = vm.Name;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
           
            var data = await _db.Tags.FindAsync(id);
            
            if (data == null) return NotFound();

            _db.Tags.Remove(data);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
