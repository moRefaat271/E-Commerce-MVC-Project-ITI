using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Identity.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
