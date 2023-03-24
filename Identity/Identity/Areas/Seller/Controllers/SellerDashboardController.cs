using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Identity.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class SellerDashboardController : Controller
    {
        //[Area("Seller")]
        //[Route("Seller/[Controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
