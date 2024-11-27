using Microsoft.AspNetCore.Mvc;

namespace ProniaMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
