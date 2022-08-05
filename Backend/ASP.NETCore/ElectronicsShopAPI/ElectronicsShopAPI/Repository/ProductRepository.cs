using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;

namespace ElectronicsShopAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ElectronicsShopDbContext context) : base(context)
        {
        }
    }
}
