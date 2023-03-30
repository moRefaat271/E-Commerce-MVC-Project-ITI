using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
