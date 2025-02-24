﻿using E_Commerce.Models;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Identity.Data
{
    public class AppUser:IdentityUser
    {
        
        [MaxLength(50,ErrorMessage ="please Enter shorter name ")]

        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Name Must be only characters")]
        public  string? Name { get; set; }

        public string? ProfilePicture { get; set; }

        public ICollection<Order>? Orders { get; set; } = new HashSet<Order>();
        //
        public virtual Cart? Cart { get; set; }
    }
}
