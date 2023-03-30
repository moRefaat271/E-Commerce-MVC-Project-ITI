using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
