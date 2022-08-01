using ElectronicsShopAPI.Data;

namespace ElectronicsShopAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        IDiscountRepository Discounts { get; }
        IDiscountTypeRepository DiscountTypes { get; }
        //IUserRepository Users { get; }
        void Save();
        
    }
}
