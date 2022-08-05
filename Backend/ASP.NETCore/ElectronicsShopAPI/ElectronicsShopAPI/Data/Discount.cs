using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShopAPI.Data
{
    public class Discount
    {
        public float DiscountPercentage { get; set; }
        [ForeignKey("DiscountType")]
        public int DiscountTypeId { get; set; }
        public DiscountType? DiscountType { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
