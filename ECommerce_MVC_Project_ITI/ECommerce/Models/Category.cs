using E_Commerce.Models;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;


namespace Identity.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = " Category Name Must be only characters")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [NotMapped]
        [Display(Name = "Category Image")]
        public IFormFile ImageFile { get; set; }
        //[System.ComponentModel.DataAnnotations.Required]
        public string Image { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new HashSet<Product>();
    }
}
