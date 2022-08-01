using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShopAPI.Data
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        [Range(0, float.PositiveInfinity, ErrorMessage = "Price must be positive value")]
        public float Price { get; set; }
        [Required]
        public string? Description { get; set; }
        [Url]
        public string? ImageURL { get; set; }
        public List<DiscountDTO>? Discounts { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}
