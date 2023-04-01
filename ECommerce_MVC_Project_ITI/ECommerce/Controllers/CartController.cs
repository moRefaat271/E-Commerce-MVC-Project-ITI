using E_Commerce.Models;
using ECommerce.Models;
using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CartController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = _context.Carts.Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefault(c => c.AppUserId == user.Id);

            //if (cart == null)
            //{
            //    return NotFound();
            //}
            //var cart = _context.Carts.FirstOrDefault(c => c.AppUserId == user.Id);
            //var prdsid = _context.CartProducts.Where(cp => cp.CartId == cart.Id).Select(cp=>cp.ProductId).ToList() ;
            //var prdList = new List<Product>();

            //foreach(var id in prdsid)
            //{
            //    prdList.Add(_context.Products.Find(id));
            //}

            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity, decimal price)
        {
            var user = await _userManager.GetUserAsync(User);

            var product = _context.Products.Find(productId);

            var cart = _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.AppUserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    AppUserId = user.Id,
                    CartProducts = new List<CartProduct>()
                };
                _context.Carts.Add(cart);
            }

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

            if (cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    Cart = cart,
                    Product = product,
                    Quantity = quantity,
                    Price = price
                };
                cart.CartProducts.Add(cartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);

            var product = _context.Products.Find(productId);

            var cart = _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.AppUserId == user.Id);

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

            if (cartProduct != null)
            {
                if(quantity <= 0)
                    _context.CartProducts.Remove(cartProduct);
                else 
                    cartProduct.Quantity = quantity;
            }


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/RemoveFromCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.AppUserId == user.Id);

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
