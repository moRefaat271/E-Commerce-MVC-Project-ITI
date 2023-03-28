using BIM_App.Servicies;
using E_Commerce.Models;
using Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI().AddDefaultTokenProviders();
             

            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/Error");
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseSession();
            app.UseRouting();

            app.UseAuthentication();// check cookie
            app.UseAuthorization();//check Role

            //-----------------------------------------------------------------------------------------------------------
            // Seed the database with a static Admin user if it doesn't exist
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminEmail = "admin@example.com";
            var adminPassword = "P@ssw0rd";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                if (roleManager.FindByNameAsync("Admin").Result == null)
                {
                    var adminRole = new IdentityRole("Admin");
                    roleManager.CreateAsync(adminRole).Wait();
                }

                var adminUser = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Admin"
                };
                var result = userManager.CreateAsync(adminUser, adminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
            //------------------------------------------------------------------------------------------------------------




            //-----------------------------------------------------------------------------------------------------------
            // Seed the database with a static seller user if it doesn't exist
            using var scopeSeller = app.Services.CreateScope();
            var userManagerSeller = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManagerSeller = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var SellerEmail = "Seller@example.com";
            var SellerPassword = "P@ssw0rd";
            if (userManagerSeller.FindByEmailAsync(SellerEmail).Result == null)
            {
                if (roleManagerSeller.FindByNameAsync("Seller").Result == null)
                {
                    var Seller = new IdentityRole("Seller");
                    roleManagerSeller.CreateAsync(Seller).Wait();
                }

                var SellerUser = new Seller
                {
                    UserName = SellerEmail,
                    Email = SellerEmail,
                    Name = "Seller"
                };
                var result = userManagerSeller.CreateAsync(SellerUser, SellerPassword).Result;

                if (result.Succeeded)
                {
                    userManagerSeller.AddToRoleAsync(SellerUser, "Seller").Wait();
                }
            }
            //------------------------------------------------------------------------------------------------------------





            app.MapAreaControllerRoute(
                name: "Admin_default",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "Seller_default",
                areaName: "Seller",
                pattern: "Seller/{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}