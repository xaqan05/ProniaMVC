using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaMVC.DataAcces;
using ProniaMVC.Extensions;
using ProniaMVC.Models;
using ProniaMVC.ViewModels.Slider;

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
            Slider slider = new Slider();

            if (vm == null)
            {
                ModelState.AddModelError("File", "File is required or file size exceeds the limit.");
                return View(vm);
            }


            if (vm.File != null)
            {
                if (!vm.File.IsValidType("image"))
                    ModelState.AddModelError("File", "File must be image");
                if (!vm.File.IsValidSize(5 * 1024))
                    ModelState.AddModelError("File", "File must be less than 5MB");

                string newFileName = await vm.File.UploadAsync("wwwroot", "imgs", "sliders");
                slider.ImageUrl = newFileName;
            }


            if (!ModelState.IsValid)
            {
                return View();
            }


            slider.Title = vm.Title;
            slider.Subtitle_1 = vm.Subtitle_1;
            slider.Subtitle_2 = vm.Subtitle_2;
            slider.Link = vm.Link;

            await _context.AddAsync(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var data = await _context.Sliders.FindAsync(id);
           
            if (data is null)
                return NotFound();


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
            if (!id.HasValue) return BadRequest();

            var data = await _context.Sliders.FindAsync(id);

            if (data is null) return NotFound();

            if (vm.File != null)
            {
                if (!vm.File!.IsValidType("image"))
                    ModelState.AddModelError("File", "File type must be image");

                if (!vm.File!.IsValidSize(5 * 1024))
                    ModelState.AddModelError("File", "File size must be less than 5MB");

                string oldFilePath = Path.Combine(_env.WebRootPath, "imgs", "sliders", data.ImageUrl);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                string newFileName = await vm.File.UploadAsync("wwwroot", "imgs", "sliders");
                data.ImageUrl = newFileName;
            }

            if (!ModelState.IsValid) return View(vm);

            data.Link = vm.Link;
            data.Subtitle_1 = vm.Subtitle_1;
            data.Subtitle_2 = vm.Subtitle_2;
            data.Title = vm.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Sliders.FindAsync(id);

            if (data is null) return NotFound();

            string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "imgs", "sliders");

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            _context.Sliders.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Hide(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Sliders.FindAsync(id);

            if (data is null) return NotFound();

            data.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        } 
        
        public async Task<IActionResult> Show(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var data = await _context.Sliders.FindAsync(id);

            if (data is null) return NotFound();

            data.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
