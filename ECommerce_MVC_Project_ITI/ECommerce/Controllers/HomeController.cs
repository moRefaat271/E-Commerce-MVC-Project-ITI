using Identity.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Identity.Data;
using E_Commerce.Models;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context)
        {
            _logger = logger;
            this._context = _context; 
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404)
                {
                    // return a custom 404 page
                    return View("Error404");
                }
                else
                {
                    // handle other HTTP errors
                    return View("Error");
                }
            }

            // handle unhandled exceptions
            return View("Error");
        }

        public IActionResult Index()
        {
            Random rnd = new();

            var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.Seller);
            var lst = applicationDbContext.ToList();
            int maxProductId;
            int randomProductId;

            if (lst.Count > 6)
            {
                List<Product> randomProducts = new();

                int i = 0;
                //List<int> UsedNumberInRandom = new();
                while (i <= 5)
                {
                    maxProductId = _context.Products.Max(p => p.ProductId);
                    randomProductId = rnd.Next(1, maxProductId + 1);

                    Product filteredProducts = lst.Where(P => P.ProductId == randomProductId).FirstOrDefault()!;
                    if (filteredProducts == null || randomProducts.Contains(filteredProducts))
                    {
                        i--;
                    }
                    else
                    {
                        randomProducts.Add(filteredProducts);
                    }
                    i++;
                }
                return View(randomProducts);

            }


            return View(lst);
        }



    }
}