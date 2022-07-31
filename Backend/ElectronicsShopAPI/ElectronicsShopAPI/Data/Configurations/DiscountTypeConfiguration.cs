using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicsShopAPI.Data.Configurations
{
    public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            //Add Seed Data
            builder.HasData(
                new DiscountType { DiscountTypeId = 1, DiscountTypeName = "User" },
                new DiscountType { DiscountTypeId = 2, DiscountTypeName = "2Piece" }
            );
        }
    }
}
