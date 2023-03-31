using E_Commerce.Models;
using ECommerce.Models;
using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().Property(o => o.Total).HasPrecision(20, 4);
            builder.Entity<Product>().Property(o => o.Price).HasPrecision(20, 4);
            builder.Entity<OrderProduct>().Property(o => o.Price).HasPrecision(20, 4);
            builder.Entity<CartProduct>().Property(o => o.Price).HasPrecision(20, 4);
            builder.Entity<OrderProduct>().HasKey(o => new { o.OrderId, o.ProductId });
            builder.Entity<CartProduct>().HasKey(o => new { o.CartId, o.ProductId });

            

            builder.Entity<Cart>()
            .HasMany(c => c.CartProducts)
            .WithOne(cp => cp.Cart)
            .HasForeignKey(cp => cp.CartId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasMany(p => p.CartProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<CartProduct> CartProducts { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
    }
}