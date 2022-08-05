using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicsShopAPI.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            //Set composite primary key
            builder.HasKey(c => new { c.DiscountTypeId, c.ProductId });
            //Add Seed Data
            /*
            builder.HasData(
                
            );
            */
        }
    }
}
