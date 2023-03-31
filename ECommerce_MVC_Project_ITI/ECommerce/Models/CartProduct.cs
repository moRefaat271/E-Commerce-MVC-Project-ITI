using E_Commerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [InverseProperty("CartProducts")]
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }
        [ForeignKey("ProductId")]
        [InverseProperty("CartProducts")]
        public Product? Product { get; set; }
    }
}
