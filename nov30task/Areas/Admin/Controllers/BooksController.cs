﻿using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = "SuperAdmin, Admin, Moderator")]
	public class BooksController : Controller
	{
        PustokDbContext Db { get; }
        IWebHostEnvironment WebHostEnvironment {  get; }

        public BooksController(PustokDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            Db = db;
            WebHostEnvironment = webHostEnvironment;
        }

        // Index:

        [AllowAnonymous]
        public async Task<IActionResult> Index()
		{
            var booksFromDb = await Db.Books.Select(bookFromDb => new BookListItemVM
            {
                Id = bookFromDb.Id,
                Name = bookFromDb.Name,
                CostPrice = bookFromDb.CostPrice,
                SellPrice = bookFromDb.SellPrice,
                Discount = bookFromDb.Discount,
                Category = bookFromDb.Category,
                IsDeleted = bookFromDb.IsDeleted,
                Quantity = bookFromDb.Quantity,
                About = bookFromDb.About,
                Description = bookFromDb.Description,
                ImageUrl = bookFromDb.ImageUrl,
            }).ToListAsync();

            return base.View(booksFromDb);
		}

        // Create:

        // Get
		public IActionResult Create()
		{
            ViewBag.Categories = new SelectList(Db.Categories, "Id", "Name");
            return View();
		}

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM bookCreateVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(Db.Categories, "Id", "Name");
                return View(bookCreateVM);
            }

            string path = Path.Combine("~/", "image/books");

            FileInfo fileInfo = new(bookCreateVM.ImageFile.FileName);
            
            string fileName = bookCreateVM.ImageFile + fileInfo.Extension;

            /*string randomName = */

            string fileNameWithPath = Path.Combine(path, fileName);

            using (FileStream stream = new(fileNameWithPath, FileMode.Create))
            {
                bookCreateVM.ImageFile.CopyTo(stream);
            }

            Book bookCreate = new()
            {
                Name = bookCreateVM.Name,
                About = bookCreateVM.About,
                CostPrice = bookCreateVM.CostPrice,
                Description = bookCreateVM.Description,
                Discount = bookCreateVM.Discount,
                ImageUrl = fileName,
                Quantity = bookCreateVM.Quantity,
                SellPrice = bookCreateVM.SellPrice,
                CategoryId = bookCreateVM.CategoryId,
            };

            await Db.Books.AddAsync(bookCreate);
            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Update:

        // Get
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            ViewBag.CategoryId = new SelectList(Db.Categories, "Id", "Name");

            var bookFromDb = await Db.Books.FindAsync(id);

            if (bookFromDb == null) return NotFound();

            BookUpdateVM bookUpdateViewModel = new()
            {
                Description = bookFromDb.Description,
                CategoryId = bookFromDb.CategoryId,
                About = bookFromDb.About,
                CostPrice = bookFromDb.CostPrice,
                Discount = bookFromDb.Discount,
                Name = bookFromDb.Name,
                Quantity = bookFromDb.Quantity,
                SellPrice = bookFromDb.SellPrice,
                ImageUrl = bookFromDb.ImageUrl
            };

            return View(bookUpdateViewModel);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Update(int id, BookUpdateVM bookUpdateViewModel)
        {
            if (id <= 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(Db.Categories, "Id", "Name");
                return View(bookUpdateViewModel);
            } 

            var bookFromDb = await Db.Books.FindAsync(id);

            if (bookFromDb == null) return NotFound();

            bookFromDb.Name = bookUpdateViewModel.Name;
            bookFromDb.ImageUrl = bookUpdateViewModel.ImageUrl;
            bookFromDb.Quantity = bookUpdateViewModel.Quantity;
            bookFromDb.SellPrice = bookUpdateViewModel.SellPrice;
            bookFromDb.Discount = bookUpdateViewModel.Discount;
            bookFromDb.CategoryId = bookUpdateViewModel.CategoryId;
            bookFromDb.About = bookUpdateViewModel.About;
            bookFromDb.CostPrice = bookUpdateViewModel.CostPrice;
            bookFromDb.Description = bookUpdateViewModel.Description;

            await Db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bookFromDb = await Db.Books.Include(b => b.Category).Include(b => b.ImageUrl).FirstOrDefaultAsync(b => b.Id == id);

            if (bookFromDb == null) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
