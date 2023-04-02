using E_Commerce.Models;
using Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.RepoServices
{
    public class ProductRepo:IProductRepo
    {
        public ProductRepo(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public async Task AddProductAsync(Product product)
        {
             await  Context.Products.AddAsync(product);
                Context.SaveChanges();
        }

        public async Task DeleteProductAsync(int productId)
        {
             Context.Products.Remove(Context.Products.Find(productId));
                await Context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
           return await Context.Products.Include(c=>c.Category).Include(s=>s.Seller).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int ProductId)
        {
          return await Context.Products.FindAsync(Context.Products.Find(ProductId));
        }

        public bool ProductExists(int id)
        {
            return (Context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        public async Task UpdateProductAsync(Product product)
        {
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
        }
    }
}
