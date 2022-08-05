using ElectronicsShopAPI.Data;
using ElectronicsShopAPI.IRepository;
using System.Linq.Expressions;

namespace ElectronicsShopAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ElectronicsShopDbContext _context;
        public Repository(ElectronicsShopDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetRange(int skip, int take)
        {
            return _context.Set<T>().Skip(skip).Take(take);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
