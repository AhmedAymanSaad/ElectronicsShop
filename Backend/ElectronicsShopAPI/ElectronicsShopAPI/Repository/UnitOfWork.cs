using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;

namespace ElectronicsShopAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ElectronicsShopDbContext _context;
        public UnitOfWork(ElectronicsShopDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Discounts = new DiscountRepository(_context);
            DiscountTypes = new DiscountTypeRepository(_context);
            Products = new ProductRepository(_context);
            //Users = new UserRepository(_context);

        }

        public ICategoryRepository Categories { get; private set; }

        public IProductRepository Products { get; private set; }


        public IDiscountRepository Discounts { get; private set; }


        public IDiscountTypeRepository DiscountTypes { get; private set; }
        //public IUserRepository Users { get; private set; }


        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        void IUnitOfWork.Save()
        {
            _context.SaveChanges();
        }
    }
}
