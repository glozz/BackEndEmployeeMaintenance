
using Mango.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { 
        
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Gloucester",
                Price = 40,
                CategoryName = "Appetizer",
                Description = "Return the product if not happy",
                ImageUrl = "https://cdotnetmastery.blob.core.windows.net/mango/11.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Leroy",
                Price = 25,
                CategoryName = "Packs",
                Description = "Please dont Return the product if not happy",
                ImageUrl = "https://cdotnetmastery.blob.core.windows.net/mango/12.jpg"
            }); ;
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Mafokoane",
                Price = 30,
                CategoryName = "None",
                Description = "Please dont Return the product if not happy",
                ImageUrl = "https://cdotnetmastery.blob.core.windows.net/mango/13.jpg"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Esrom",
                Price = 16,
                CategoryName = "Nice",
                Description = "Please dont Return the product if not happy",
                ImageUrl = "https://cdotnetmastery.blob.core.windows.net/mango/14.jpg"
            });
        }


    }
}
