using System.Linq.Expressions;

namespace ElectronicsShopAPI.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Read operations
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetRange(int skip, int take);

        //Create operations
        void Add(T entity);

        //Update operations -> note: update should not be used in repository architecture?
        void Update(T entity);

        //Delete operations
        void Delete(T entity);
        void Delete(int id);
    }
    
}
