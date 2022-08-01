using System.ComponentModel.DataAnnotations;

namespace ElectronicsShopAPI.Models
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
    }

    public class CreateCategoryDTO
    {
        [Required]
        public string? CategoryName { get; set; }
    }
}
