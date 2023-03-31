using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Identity.Data;
using Identity.Models;
using ECommerce.RepoServices;

namespace ECommerce.Controllers
{
    public class CategoriesController : Controller
    {

        public ICategoryRepo CatRep { get; }

        public CategoriesController(ICategoryRepo CatRep)
        {
            this.CatRep = CatRep;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return await CatRep.GetAllCategoriesAsync() != null ?View(await CatRep.GetAllCategoriesAsync()):Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == null || _context.Categories == null)
            //{
            //    return NotFound();
            //}

            var category = await CatRep.GetCategoryByIdAsync(id);
             
            if (category == null)
            {
                return NotFound();
            }
            var productList = await CatRep.GetAllProductOfOneCategoryAsync(category);

            return View(productList);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await CatRep.AddCategoryAsync(category);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == null || _context.Categories == null)
            //{
            //    return NotFound();
            //}

            var category = await CatRep.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await CatRep.UpdateCategoryAsync(category);   
                    //_context.Update(category);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( ! CatRep.CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            //if (id == null || _context.Categories == null)
            //{
            //    return NotFound();
            //}

            var category = await CatRep.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Categories == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            //}
            var category = await CatRep.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await CatRep.DeleteCategoryAsync(category.Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
