using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.DataAcces;
using ProniaMVC.Extensions;
using ProniaMVC.Models;
using ProniaMVC.ViewModels;

namespace ProniaMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(ProniaDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!vm.File.IsValidType("image"))
            {
                ModelState.AddModelError("File", "File must be image");
                return View();
            }

            if (!vm.File.IsValidSize(5 * 1024))
            {
                ModelState.AddModelError("File", "File must be less than 5MB");
                return View();
            }
            string newFileName = await vm.File.UploadAsync("wwwroot", "imgs", "sliders");

            Slider slider = new Slider
            {
                Title = vm.Title,
                Subtitle_1 = vm.Subtitle_1,
                Subtitle_2 = vm.Subtitle_2,
                Link = vm.Link,
                ImageUrl = newFileName
            };

            await _context.AddAsync(slider);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Slider? data = await _context.Sliders.FindAsync(id);

            SliderUpdateVM vm = new SliderUpdateVM();

            vm.Title = data.Title;
            vm.Subtitle_1 = data.Subtitle_1;
            vm.Subtitle_2 = data.Subtitle_2;
            vm.Link = data.Link;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            var data = await _context.Sliders.FindAsync(id);

            if (data is null) return NotFound();
            if(!ModelState.IsValid) return View();
            if (vm.File != null)
            {
                if (!vm.File.IsValidType("image"))
                {
                    ModelState.AddModelError("File", "File type must be image");
                    return View();
                }

                if (!vm.File.IsValidSize(5 * 1024))
                {
                    ModelState.AddModelError("File", "File size must be less than 5MB");
                    return View();
                }

                string oldFilePath = Path.Combine(_env.WebRootPath, "imgs", "sliders", data.ImageUrl);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                string newFileName = await vm.File.UploadAsync("wwwroot", "imgs", "sliders");
                data.ImageUrl = newFileName;
            }
            data.Link = vm.Link;
            data.Subtitle_1 = vm.Subtitle_1;
            data.Subtitle_2 = vm.Subtitle_2;
            data.Title = vm.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
