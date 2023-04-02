using E_Commerce.Models;
using Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.RepoServices
{
    public class SellerRepo : ISellerRepo
    {
        public SellerRepo(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }
        public async Task<List<Seller>> GetAllProductAsync()
        {
            return await Context.Sellers.ToListAsync();


        }

        
    }
}
