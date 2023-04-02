using ECommerce.Models;
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
        public string ProductDescription { get; set; }
        [Required]
        public int NumInStock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SellerId { get; set; }
        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        [ForeignKey("SellerId")]
        public Seller? Seller { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; } = new HashSet<OrderProduct>();
        //
        public virtual ICollection<CartProduct>? CartProducts { get; set; } = new HashSet<CartProduct>();

    }
}
