using Microsoft.AspNetCore.Mvc;
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
                ExTax = p.ExTax,
                Brand = p.Brand,
                BookCode = p.BookCode,
				RewardPoints = p.RewardPoints,
				Avability = p.Avability,
                ImageUrl = p.CoverImageUrl
            }));
		}

		public async Task<IActionResult> Create()
		{
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
		}

		[HttpPost]

        public async Task<IActionResult> Create(BookCreateVM vm)
        {

			if (vm.CostPrice > vm.SellPrice)
            {
				ModelState.AddModelError("SellPrice", "SellPrice > CostPrice olmalidir.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }
            if (!await _db.Categories.AnyAsync(c => c.Id == vm.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Bele kateqoriya yoxdur.");
                ViewBag.Categories = _db.Categories;
                return View(vm);
            }

            string fileName = Path.Combine("image", "books", vm.ImageFile.FileName);

            using (FileStream fs = System.IO.File.Create(Path.Combine(_webHostEnvironment.WebRootPath, fileName)))
            {
                await vm.ImageFile.CopyToAsync(fs);
            }

            Book book = new Book
			{
				Name = vm.Name,
				About = vm.About,
				SellPrice = vm.SellPrice,
				CostPrice = vm.CostPrice,
				Avability = vm.Avability,
                CategoryId = vm.CategoryId,
                Brand = vm.Brand,
                CoverImageUrl = fileName,
				Quantity = vm.Quantity,
				Description = vm.Description,
				Discount = vm.Discount,
                ExTax = vm.ExTax,
                BookCode = vm.BookCode,
                RewardPoints = vm.RewardPoints,
            };

			await _db.Books.AddAsync(book);
			await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id <= 0) return BadRequest();
            var data = await _db.Books.FindAsync(id);
            ViewBag.Categories = _db.Categories;
            if (data == null) return NotFound();
            return View(new BookUpdateVM
            {
                Name = data.Name,
                About = data.About,
                Avability = data.Avability,
                Brand = data.Brand,
                CategoryId = data.CategoryId,
                CostPrice = data.CostPrice,
                Description = data.Description,
                Discount = data.Discount,
                ExTax = data.ExTax,
                BookCode = data.BookCode,
                Quantity = data.Quantity,
                RewardPoints = data.RewardPoints,
                ImageUrl = data.CoverImageUrl,
                SellPrice = data.SellPrice
            });
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, BookUpdateVM vm)
        {
            TempData["BookRenovationResponse"] = false;

            if (id == null || id <= 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Books.FindAsync(id);
            if (data == null) return NotFound();

            string fileName = Path.Combine("image", "books", vm.ImageFile.FileName);

            using (FileStream fs = System.IO.File.Create(Path.Combine(_webHostEnvironment.WebRootPath, fileName)))
            {
                await vm.ImageFile.CopyToAsync(fs);
            }

            data.Name = vm.Name;
            data.About = vm.About;
            data.Avability = vm.Avability;
            data.Brand = vm.Brand;
            data.CostPrice = vm.CostPrice;
            data.Description = vm.Description;
            data.Discount = vm.Discount;
            data.ExTax = vm.ExTax;
            data.BookCode = vm.BookCode;
            data.Quantity = vm.Quantity;
            data.RewardPoints = vm.RewardPoints;
            data.SellPrice = vm.SellPrice;
            data.CategoryId = vm.CategoryId;
            data.CoverImageUrl = fileName;

            TempData["BookRenovationResponse"] = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            TempData["BookDeletionResponse"] = false;
            if (id == null) return BadRequest();
            var data = await _db.Books.FindAsync(id);
            if (data == null) return NotFound();
            _db.Books.Remove(data);
            await _db.SaveChangesAsync();
            TempData["BookDeletionResponse"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
