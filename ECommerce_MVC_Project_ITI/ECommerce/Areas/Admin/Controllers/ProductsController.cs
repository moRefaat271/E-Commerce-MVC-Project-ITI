using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
using Identity.Data;
using BIM_App.Servicies;
using Identity.Models;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ProductsController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            this._fileService = fileService;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.Seller);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name");

            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductDescription,NumInStock,Price,SellerId,ImageFile,CategoryId")] Product product)
        {
            product.Image = product.ImageFile.FileName;
            if (product.ImageFile != null)
            {
                var result = _fileService.SaveImage(product.ImageFile);
                if (result.Item1 == 1)
                {
                    var oldImage = product.Image;
                    product.Image = "/uploads/" + result.Item2;
                    var deleteResult = _fileService.DeleteImage(oldImage);
                }
            }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Image", product.CategoryId);
            //ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Id", product.SellerId);
            //return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.Name);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Name", product.Name);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductDescription,NumInStock,Price,SellerId,ImageFile,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (product != null)
                product.Image = product.ImageFile.FileName;
            /*            if (ModelState.IsValid)
                        {*/
            try
                {
                    if (product.ImageFile != null)
                    {
                        var result = _fileService.SaveImage(product.ImageFile);
                        if (result.Item1 == 1)
                        {
                            var oldImage = product.Image;
                            product.Image = "/uploads/" + result.Item2;
                            var deleteResult = _fileService.DeleteImage(oldImage);
                        }
                    }
                     _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}*/
/*            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Image", product.CategoryId);
            ViewData["SellerId"] = new SelectList(_context.Sellers, "Id", "Id", product.SellerId);
            return View(product);*/
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
