using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nov30task.Areas.Admin.ViewModels;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.BooksVM;

namespace nov30task.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BooksController : Controller
	{

		PustokDbContext _db { get; }

        IWebHostEnvironment _webHostEnvironment {  get; }

        public BooksController(PustokDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
		{
			return base.View(_db.Books.Select(p => new AdminBookListItemVM{
				Id = p.Id,
				Name = p.Name,
				CostPrice = p.CostPrice,
				SellPrice = p.SellPrice,
				Discount = p.Discount,
                Category = p.Category,
                IsDeleted = p.IsDeleted,
				Quantity = p.Quantity,
				About = p.About,
                Description = p.Description,
            }));
		}

		public async Task<IActionResult> Create()
		{
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
		}

		[HttpPost]

        public async Task<IActionResult> Create([Bind("Id,Name,About,Description,ExTax,Brand,BookCode,RewardPoints,Avability,SellPrice,CostPrice,Discount,CoverImageUrl,Quantity,CategoryId,IsDeleted")] Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,About,Description,ExTax,Brand,BookCode,RewardPoints,Avability,SellPrice,CostPrice,Discount,CoverImageUrl,Quantity,CategoryId,IsDeleted")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(book);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }*/

        /*public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var book = await _db.Books.Include(b => b.Category).Include(b => b.BookImage).FirstOrDefaultAsync(b => b.Id == id);
            //if (book == null)
            //{
            //    return NotFound();
            //}

            //return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _db.Books.Any(b => b.Id == id);
        }*/
    }
}
