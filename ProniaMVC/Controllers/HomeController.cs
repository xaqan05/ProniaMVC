using Microsoft.AspNetCore.Mvc;

namespace ProniaMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
