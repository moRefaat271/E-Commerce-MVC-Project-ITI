using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Identity.Models;
using ECommerce.Models;


namespace ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var user =  await _userManager.GetUserAsync(User);

            var cart = _context.Carts?.Include(c => c.CartProducts).ThenInclude(p => p.Product).FirstOrDefault(c => c.AppUserId == user.Id);


            ViewBag.ProductsOfCart = cart?.CartProducts?.ToList();
            ViewData["ProductsOfCart"] = cart?.CartProducts?.ToList();
           
            ViewData["AppUserId"] = new SelectList(_context.AppUsers.Where(u=> u.Id==user.Id).Take(1), "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int OrderId,string Street, string City, string Country,int PostalCode,decimal Total,string AppUserId )
        {
            Order order=new();
            if (ModelState.IsValid)
            {
                 order = new Order()
                 {
                    OrderDate = DateTime.Now,
                    Street = Street,
                    City = City,
                    Country = Country,
                    PostalCode = PostalCode,
                    Total = Total,
                    AppUserId = AppUserId
                 };
                _context.Add(order);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
            }
            var user = await _userManager.GetUserAsync(User);
            var cart = _context.Carts?.Include(c => c.CartProducts).ThenInclude(p => p.Product).FirstOrDefault(c => c.AppUserId == user.Id);

            var cartProductsList = cart?.CartProducts?.ToList();

                decimal sum = 0;
          if(cartProductsList!=null)
            {
                foreach (var cartProduct in cartProductsList)
            {
                    order?.OrderProducts?.Add(
                    new OrderProduct()
                    {
                        Quantity = cartProduct.Quantity,
                        Price= cartProduct.Price,
                        ProductId= cartProduct.ProductId,
                        OrderId= cartProduct.ProductId
                    }
                    );
                    sum += (cartProduct.Product.Price * cartProduct.Quantity);
            }

            }

            try
            {
                
                var orderToEdit = _context.Orders
                    .Where(o => o.AppUserId == user.Id)
                    .OrderByDescending(o => o.OrderDate)
                    .FirstOrDefault();
                if (orderToEdit != null)
                    orderToEdit.Total = sum;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "PayPal");
            }
            catch
            {
                return RedirectToAction("Error", "home");
            }
            //ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", order.AppUserId);
            //return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", order.AppUserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,Street,City,Country,PostalCode,Total,AppUserId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", order.AppUserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
