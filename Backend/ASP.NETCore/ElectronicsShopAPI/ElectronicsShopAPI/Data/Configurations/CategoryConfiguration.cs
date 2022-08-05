using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicsShopAPI.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //Add Seed Data
            builder.HasData(
                new Category { CategoryId = 1, CategoryName = "TV" },
                new Category { CategoryId = 2, CategoryName = "Laptop" },
                new Category { CategoryId = 3, CategoryName = "Sound System" }
            );
        }
    }
}
