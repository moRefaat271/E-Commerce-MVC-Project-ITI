using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize]
    public class PayPalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View("PaymentSuccess");
        }
    }
}