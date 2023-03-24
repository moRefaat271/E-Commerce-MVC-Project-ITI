using Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Seller: AppUser
    {
        public virtual List<Product>? Products { get; set; }
    }
}
