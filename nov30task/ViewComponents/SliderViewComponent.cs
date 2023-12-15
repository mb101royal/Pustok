using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nov30task.Context;
using nov30task.ViewModels.SlidersVM;

namespace nov30task.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {

        PustokDbContext _context { get; }

        public SliderViewComponent(PustokDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Sliders.Select(s => new SliderListItemVM
            {
                Id = s.Id,
                ButtonText = s.ButtonText,
                ImageUrl = s.ImageUrl,
                Position = s.Position,
                Text = s.Text,
                Title = s.Title
            }).ToListAsync());
        }

    }
}
