using ElectronicsShopAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShopAPI.Models
{
    public class DiscountDTO
    {
        [Required]
        [Range(0, 100, ErrorMessage = "Discount Value must be percentage between 0 and 100")]
        public float DiscountPercentage { get; set; }
        
        [ForeignKey("DiscountType")]
        public int DiscountTypeId { get; set; }
        [Required]
        public DiscountType? DiscountType { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public Product? Product { get; set; }

    }
}
