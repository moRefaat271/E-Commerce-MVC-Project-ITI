using Microsoft.AspNetCore.Mvc;

namespace Identity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDashboardController : Controller
    {
        //[Area("Admin")]
        //[Route("Admin/[Controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
