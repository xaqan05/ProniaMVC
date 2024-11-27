using Microsoft.AspNetCore.Mvc;

namespace ProniaMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
