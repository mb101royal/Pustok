using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;

namespace nov30task.Controllers
{
    public class BookController : Controller
    {

        PustokDbContext _context { get; }

        public BookController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            var data = await _context.Books.Select(b => new BookDetailVM
            {
                Discount = b.Discount,

            }).SingleOrDefaultAsync(p => p.Id == id);

            if (data != null) return NotFound();

            return View(data);
        }*/

        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null || id <= 0) return BadRequest();

            if (!await _context.Books.AnyAsync(b => b.Id == id)) return NotFound();

            HttpContext.Response.Cookies.Append("basket", id.ToString());

            return Ok();
        }
    }
}
