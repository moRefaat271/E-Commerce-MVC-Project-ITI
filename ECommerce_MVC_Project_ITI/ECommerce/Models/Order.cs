using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Identity.Models;
using Identity.Data;

namespace E_Commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public string Street { get; set; }
        [Required]
        public string  City { get; set; }
        [Required]
        public string  Country { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public int  PostalCode { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser? AppUser { get; set; }

        public ICollection<OrderProduct>? OrderProducts { get; set; } = new HashSet<OrderProduct>();

        [NotMapped]
        public bool? PaymentSuccess { get; set; }
    }
}
