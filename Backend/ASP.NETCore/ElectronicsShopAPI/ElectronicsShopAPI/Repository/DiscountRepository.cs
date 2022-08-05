using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;

namespace ElectronicsShopAPI.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(ElectronicsShopDbContext context) : base(context)
        {
        }
    }
}
