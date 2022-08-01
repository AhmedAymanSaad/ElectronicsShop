using System.ComponentModel.DataAnnotations;

namespace ElectronicsShopAPI.Data
{
    public class DiscountTypeDTO
    {
        public int DiscountTypeId { get; set; }
        [Required]
        public string? DiscountTypeName { get; set; }
    }
}
