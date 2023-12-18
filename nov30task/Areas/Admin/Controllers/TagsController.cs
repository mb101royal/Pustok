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

        PustokDbContext Db { get; }

        public TagsController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        public IActionResult Index()
        {

            var tagsFromDb = Db.Tags.Select(tag => new TagListItemVM
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

            var nameInDb = await Db.Tags.FirstOrDefaultAsync(n=> n.Name == vm.Name);

            if (nameInDb != null)
            {
                if (nameInDb.Name != vm.Name)
                {
                    Tag tagToCreate = new() { Name = vm.Name };

                    await Db.Tags.AddAsync(tagToCreate);
                    await Db.SaveChangesAsync();

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
                Tag tagToCreate = new() { Name = vm.Name };

                await Db.Tags.AddAsync(tagToCreate);
                await Db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var tagFromDb = await Db.Tags.FindAsync(id);

            if (tagFromDb == null) return NotFound();

            var tagToUpdate = new TagUpdateVM { Name = tagFromDb.Name };

            return View(tagToUpdate);
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, TagUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var tagFromDb = await Db.Tags.FindAsync(id);
            
            if (tagFromDb == null) return NotFound();

            var nameInDb = await Db.Tags.FirstOrDefaultAsync(n => n.Name == vm.Name);

            if (nameInDb != null)
            {
                if (nameInDb.Name != vm.Name)
                {
                    tagFromDb.Name = vm.Name;

                    await Db.SaveChangesAsync();

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
                tagFromDb.Name = vm.Name;

                await Db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
           
            var tagFromDb = await Db.Tags.FindAsync(id);
            
            if (tagFromDb == null) return NotFound();

            Db.Tags.Remove(tagFromDb);

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
