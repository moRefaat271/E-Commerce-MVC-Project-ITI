using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    public class SellerDashboardController : Controller
    {
        public ApplicationDbContext _context { get; }
        public UserManager<AppUser> _userManager { get; }

        public SellerDashboardController(ApplicationDbContext _context, UserManager<AppUser> userManager) {
            this._context = _context;
            this._userManager = userManager;
        }


        //[Area("Seller")]
        //[Route("Seller/[Controller]")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
