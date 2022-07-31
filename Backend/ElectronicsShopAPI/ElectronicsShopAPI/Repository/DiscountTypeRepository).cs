using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;

namespace ElectronicsShopAPI.Repository
{
    public class DiscountTypeRepository : Repository<DiscountType>, IDiscountTypeRepository
    {
        public DiscountTypeRepository(ElectronicsShopDbContext context) : base(context)
        {
        }
    }
}
