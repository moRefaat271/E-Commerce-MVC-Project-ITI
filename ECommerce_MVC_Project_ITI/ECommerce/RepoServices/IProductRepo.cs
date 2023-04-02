using E_Commerce.Models;
using Identity.Models;

namespace ECommerce.RepoServices
{
    public interface IProductRepo
    {
        public Task<List<Product>> GetAllProductAsync();
        public Task<Product> GetProductByIdAsync(int categoryId);
        public bool ProductExists(int id);

        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(int productId);
    }
}
