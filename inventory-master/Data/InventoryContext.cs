using InventorySystem.data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InventorySystem.Data
{
    public class InventoryContext : IdentityDbContext<ApplicationUser>
    {
        IConfiguration config;
        public InventoryContext(IConfiguration _config)
        {
            config = _config;
        }
        public DbSet<Category> Categorys { set; get; }
        public DbSet<Company> Companies { set; get; }
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Orders> Orders { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Stores> Stores { set; get; }
        public DbSet<Supplier> Suppliers { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("InventoryConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
