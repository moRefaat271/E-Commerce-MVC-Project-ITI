using E_Commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
