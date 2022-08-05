using System.ComponentModel.DataAnnotations;

namespace ElectronicsShopAPI.Models
{
    public class DiscountTypeDTO
    {
        public int DiscountTypeId { get; set; }
        [Required]
        public string? DiscountTypeName { get; set; }
    }
}
