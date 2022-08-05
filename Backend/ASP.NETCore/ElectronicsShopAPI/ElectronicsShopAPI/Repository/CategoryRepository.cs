using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;

namespace ElectronicsShopAPI.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ElectronicsShopDbContext context) : base(context)
        {
        }
    }
}
