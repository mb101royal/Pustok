using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.SlidersVM;
using nov30task.ViewModels.TagsVM;

namespace nov30task.Areas.Admin.Controllers
{
    [Area("Admin")]
    /*[Authorize(Roles = "SuperAdmin, Admin, Moderator")]*/
	public class TagsController : Controller
    {
        PustokDbContext Db { get; }

        public TagsController(PustokDbContext db)
        {
            Db = db;
        }

        // Index:

        [AllowAnonymous]
        public IActionResult Index()
		{

            var tagsFromDb = Db.Tags.Select(tagFromDb => new TagListItemVM
            {
                Id = tagFromDb.Id,
                Name = tagFromDb.Name
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
        public async Task<IActionResult> Create(TagCreateVM tagCreateViewModel)
        {
            if (!ModelState.IsValid) return View(tagCreateViewModel);

            var nameInDb = await Db.Tags.FirstOrDefaultAsync(n=> n.Name == tagCreateViewModel.Name);

            if (nameInDb != null)
            {
                if (nameInDb.Name != tagCreateViewModel.Name)
                {
                    Tag tagCreate = new() { Name = tagCreateViewModel.Name };

                    await Db.Tags.AddAsync(tagCreate);
                    await Db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Name is repeated");
                    return View(tagCreateViewModel);
                }
            }
            else
            {
                Tag tagCreate = new() { Name = tagCreateViewModel.Name };

                await Db.Tags.AddAsync(tagCreate);
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

            var tagUpdateViewModel = new TagUpdateVM { Name = tagFromDb.Name };

            return View(tagUpdateViewModel);
        }
        
        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int? id, TagUpdateVM tagUpdateViewModel)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(tagUpdateViewModel);

            var tagFromDb = await Db.Tags.FindAsync(id);
            
            if (tagFromDb == null) return NotFound();

            var nameInDb = await Db.Tags.FirstOrDefaultAsync(tagFromDb => tagFromDb.Name == tagUpdateViewModel.Name);

            if (nameInDb != null)
            {
                if (nameInDb.Name != tagUpdateViewModel.Name)
                {
                    tagFromDb.Name = tagUpdateViewModel.Name;

                    await Db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Name is repeated");
                    return View(tagUpdateViewModel);
                }
            }
            else
            {
                tagFromDb.Name = tagUpdateViewModel.Name;

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
