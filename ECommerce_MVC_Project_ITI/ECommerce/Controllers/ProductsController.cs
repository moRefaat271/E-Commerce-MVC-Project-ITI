using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Models;
using Identity.Data;
using Microsoft.AspNetCore.Authorization;
using ECommerce.RepoServices;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {

        public IProductRepo ProductRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public ISellerRepo sellerRepo { get; }

        public ProductsController(IProductRepo productRepo,ICategoryRepo categoryRepo, ISellerRepo sellerRepo)
        {
            ProductRepo = productRepo;
            CategoryRepo = categoryRepo;
            this.sellerRepo = sellerRepo;
        }



        // GET: Products
        public async Task<IActionResult> Index()
        {
            return await ProductRepo.GetAllProductAsync() != null ? View(await ProductRepo.GetAllProductAsync()) : Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || ProductRepo.GetAllProductAsync == null)
            {
                return NotFound();
            }

            var product = ProductRepo.GetProductByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id");
            
            
            ViewData["SellerId"] = new SelectList((System.Collections.IEnumerable)sellerRepo.GetAllProductAsync(), "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Price,SellerId,Image,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepo.GetAllProductAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id");
            
            
            ViewData["SellerId"] = new SelectList((System.Collections.IEnumerable)sellerRepo.GetAllProductAsync(), "Id", "Id", product.SellerId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || ProductRepo.GetAllProductAsync() == null)
            {
                return NotFound();
            }

            var product = await ProductRepo.GetProductByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id");
           
         
             ViewData["SellerId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id", product.SellerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price,SellerId,Image,CategoryId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  ProductRepo.UpdateProductAsync(product);
                   
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
            }
            ViewData["CategoryId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id");
         
             ViewData["SellerId"] = new SelectList((System.Collections.IEnumerable)CategoryRepo.GetAllCategoriesAsync(), "Id", "Id", product.SellerId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || ProductRepo.GetAllProductAsync() == null)
            {
                return NotFound();
            }

            var product = ProductRepo.GetAllProductAsync();
               
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ProductRepo.GetAllProductAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await ProductRepo.GetProductByIdAsync(id);
            if (product != null)
            {
                ProductRepo.DeleteProductAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return ProductRepo.ProductExists(id);
        }
    }
}
