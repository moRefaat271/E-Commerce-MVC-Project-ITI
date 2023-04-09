using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var order =_context.Orders.Where(o => o.AppUserId == user.Id).OrderByDescending(o => o.OrderDate).FirstOrDefault();
            ViewData["IsSuccessed"] = order;
            return View(order);
        }

        public async Task<IActionResult> Success()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = _context.Carts?.Include(c => c.CartProducts)!.ThenInclude(p => p.Product).FirstOrDefault(c => c.AppUserId == user.Id);
            var order = _context.Orders?.Where(o => o.AppUserId == user.Id)
                        .OrderByDescending(o => o.OrderDate)
                        .FirstOrDefault();
            ViewBag.order = order;
            return View("PaymentSuccess", cart);
        }
    }
}