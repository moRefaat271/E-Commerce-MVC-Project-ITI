using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Identity.Models;
using Identity.Data;

namespace E_Commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required string ShippingAddress { get; set; }
        public decimal Total { get; set; }
        public required string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
