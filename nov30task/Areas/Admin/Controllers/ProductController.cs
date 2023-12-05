using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Areas.Admin.ViewModels;
using nov30task.Context;
using nov30task.Models;
using nov30task.ViewModels.ProductVM;

namespace nov30task.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{

		ApplicationDbContext _db { get; }

		public ProductController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View(_db.Products.Select(p => new AdminProductListItemVM{
				Id = p.Id,
				Name = p.Name,
				CostPrice = p.CostPrice,
				SellPrice = p.SellPrice,
				Discount = p.Discount,
				Category = p.Category,
				ImageUrl = p.ImageUrl,
				IsDeleted = p.IsDeleted,
				Quantity = p.Quantity
			}));
		}

		public IActionResult Create()
		{
			ViewBag.Categories = _db.Categories;
			return View();
		}

		[HttpPost]

        public async Task<IActionResult> Create(ProductCreateVM vm)
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

            Product product = new Product
			{
				Name = vm.Name,
				About = vm.About,
				SellPrice = vm.SellPrice,
				CostPrice = vm.CostPrice,
				Avability = vm.Avability,
				Brand = vm.Brand,
				Quantity = vm.Quantity,
				Description = vm.Description,
				Discount = vm.Discount,
				ImageUrl = vm.ImageUrl,
				CategoryId = vm.CategoryId
			};

			await _db.Products.AddAsync(product);
			await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
