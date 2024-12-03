using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.DataAcces;
using ProniaMVC.ViewModels.Common;
using ProniaMVC.ViewModels.Slider;

namespace ProniaMVC.Controllers
{
    public class HomeController(ProniaDbContext _context) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM();

            vm.Sliders = await _context.Sliders
                .Where(x => !x.IsDeleted)
                .Select(x => new SliderItemVM
                {
                    Title = x.Title,
                    Subtitle_1 = x.Subtitle_1,
                    Subtitle_2 = x.Subtitle_2,
                    Link = x.Link,

                }).ToListAsync();

            return View(vm);
        }
    }
}
