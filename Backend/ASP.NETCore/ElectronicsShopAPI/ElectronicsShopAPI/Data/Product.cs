using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicsShopAPI.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public float Price { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public virtual IList<Discount>? Discounts { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
