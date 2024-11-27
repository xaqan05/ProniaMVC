using Microsoft.AspNetCore.Mvc;

namespace ProniaMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
