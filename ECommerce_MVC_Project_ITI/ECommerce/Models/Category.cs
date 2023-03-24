using E_Commerce.Models;
using System.Security.Cryptography.X509Certificates;

namespace Identity.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new HashSet<Product>();
    }
}
