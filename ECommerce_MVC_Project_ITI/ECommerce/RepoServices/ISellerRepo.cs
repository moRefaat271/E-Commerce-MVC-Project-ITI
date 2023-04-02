using E_Commerce.Models;

namespace ECommerce.RepoServices
{
    public interface ISellerRepo
    {
        public Task<List<Seller>> GetAllProductAsync();
    }
}
