using E_Commerce.Models;
using Identity.Data;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.RepoServices
{
    public class CategoryRepoService : ICategoryRepo
    {
        public ApplicationDbContext CatContext { get; }
        public CategoryRepoService(ApplicationDbContext catContext)
        {
            CatContext = catContext;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
           return await CatContext.Categories.ToListAsync();
        }

        public bool CategoryExists(int id)
        {
            return (CatContext.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<List<Product>> GetAllProductOfOneCategoryAsync(Category category)
        {
            return await CatContext.Products.Where(p => p.CategoryId == category.Id).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await CatContext.Categories.FindAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await CatContext.Categories.AddAsync(category);
            CatContext.SaveChanges();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
             CatContext.Categories.Update(category);
            await CatContext.SaveChangesAsync();    
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            CatContext.Categories.Remove(CatContext.Categories.Find(categoryId));
            await CatContext.SaveChangesAsync();
        }





        //// this function return list of categories ... 
        //public List<Category> GetAllCategories()
        //{
        //    return CatContext.Categories.ToList();
        //}

        //// this function get details for specific category ...
        //public  Category GetCategoryById(int id)
        //{
        //    return  CatContext.Categories.Find(id);
        //}

        //// this function insert new category ... 
        //public void CreateNewCategory(Category Cat)
        //{
        //    CatContext.Categories.Add(Cat);
        //    CatContext.SaveChanges();   

        //}

        //// this function can update specific category ...
        //public void UpdateCategory(int id, Category Cat)
        //{
        //    Category modifiedCat = CatContext.Categories.Find(id);
        //    modifiedCat.Name = Cat.Name;
        //    modifiedCat.Image = Cat.Image;

        //    CatContext.SaveChanges();
        //}

        //// this function can delete specific category ...
        //public void DeleteCategory(int id)
        //{
        //    CatContext.Remove(CatContext.Categories.Find(id));
        //    CatContext.SaveChanges() ;

        //}






    }

       
    }
