using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize]
    public class PayPalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PayPalController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult >Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var money =_context.Orders.Where(o => o.AppUserId == user.Id).OrderByDescending(o => o.OrderDate).FirstOrDefault();
            return View();
        }
    }
}