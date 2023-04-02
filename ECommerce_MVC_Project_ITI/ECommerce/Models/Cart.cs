using E_Commerce.Models;
using Identity.Data;
using Identity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<CartProduct>? CartProducts { get; set; } = new HashSet<CartProduct>();
    }
}
