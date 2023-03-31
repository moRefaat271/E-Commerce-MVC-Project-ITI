using E_Commerce.Models;
using Identity.Models;

namespace ECommerce.RepoServices
{
    public interface ICategoryRepo
    {
        public  Task<List<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int categoryId);
        public Task<List<Product>> GetAllProductOfOneCategoryAsync(Category category);
        public bool CategoryExists(int id);

        public Task AddCategoryAsync(Category category);
        public  Task UpdateCategoryAsync(Category category);
         public  Task DeleteCategoryAsync(int categoryId);


    }
}
