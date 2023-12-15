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

        // Index:

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
                ImageUrl = p.ImageUrl,
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
                CategoryId = vm.CategoryId,

            };

            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);

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

            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", book.CategoryId);
            
            var getBook = new BookUpdateVM
            {
                About = book.About,
                CostPrice = book.CostPrice,
                Description = book.Description,
                CategoryId = book.CategoryId,
                Discount = book.Discount,
                ImageUrl = book.ImageUrl,
                Name = book.Name,
                Quantity = book.Quantity,
                SellPrice = book.SellPrice,
            };

            return View(getBook);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int id, BookUpdateVM vm)
        {
            if (id <= 0) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            var book = await _db.Books.FindAsync(id);

            if (book == null) return NotFound();

            book.Name = vm.Name;
            book.ImageUrl = vm.ImageUrl;
            book.Quantity = vm.Quantity;
            book.SellPrice = vm.SellPrice;
            book.Discount = vm.Discount;
            book.CategoryId = vm.CategoryId;
            book.About = vm.About;
            book.CostPrice = vm.CostPrice;
            book.Description = vm.Description;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _db.Books.Include(b => b.Category).Include(b => b.ImageUrl).FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
