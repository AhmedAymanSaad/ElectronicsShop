using ElectronicsShopAPI.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShopAPI.Data
{
    public class ElectronicsShopDbContext: IdentityDbContext<User>
    {
        public ElectronicsShopDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<DiscountType>? DiscountTypes { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Apply Configurations for Data
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }
    }
}
