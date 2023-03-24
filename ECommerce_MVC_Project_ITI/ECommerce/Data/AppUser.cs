using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Identity.Data
{
    public class AppUser:IdentityUser
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(50,ErrorMessage ="please Enter shorter name ")]
        public  string? Name { get; set; }


        public string? ProfilePicture { get; set; }

        public ICollection<Order>? Orders { get; set; } = new HashSet<Order>();
    }
}
