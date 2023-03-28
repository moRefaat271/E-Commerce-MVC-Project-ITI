using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PayPalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}