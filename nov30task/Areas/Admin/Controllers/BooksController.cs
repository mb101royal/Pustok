using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.BooksVM;
using nov30task.ViewModels.SlidersVM;

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

        public async Task<IActionResult> Index()
		{
            var booksFromDb = await _db.Books.Select(p => new BookListItemVM
            {
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
            }).ToListAsync();

            return base.View(booksFromDb);
		}

        // Create:

        // Get
		public IActionResult Create()
		{
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View();
		}

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Book book = new Book
            {
                Name = vm.Name,
                About = vm.About,
                CostPrice = vm.CostPrice,
                Description = vm.Description,
                Discount = vm.Discount,
                ImageUrl = vm.ImageUrl,
                Quantity = vm.Quantity,
                SellPrice = vm.SellPrice,
                // Error here:
                Id = vm.CategoryId
            };

            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var book = await _db.Books.FindAsync(id);

            if (book == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);

            return View(book);
        }

        // Post
        /*[HttpPost]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.Id) return NotFound();



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
