using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;

namespace nov30task.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class BooksController : Controller
    {

        ApplicationDbContext _db { get; }

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            /*await _db.Categories.Select(c => new CategoryListItemVM { Id = c.Id, Name = c.Name }).ToListAsync();*/
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Response"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Products.FindAsync(id);
            if (data == null) return NotFound();
            _db.Products.Remove(data);
            await _db.SaveChangesAsync();
            TempData["Response"] = true;
            return RedirectToAction(nameof(Index));
        }



    }
}
