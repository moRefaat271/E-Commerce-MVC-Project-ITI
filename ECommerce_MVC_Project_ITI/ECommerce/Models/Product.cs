using Identity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public required string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public Seller? Seller { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; } = new HashSet<OrderProduct>();
    }
}
