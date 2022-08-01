using System.ComponentModel.DataAnnotations;

namespace ElectronicsShopAPI.Data
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
    }
}
