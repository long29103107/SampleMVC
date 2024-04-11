using System.Linq.Expressions;

namespace SampleMVC.Repositories;

public interface IGenericRepository<T>
{
    IQueryable<T> FindAll(bool tracking = false);
    IQueryable FindByCondition(Expression<Func<T, bool>> expressions = null, bool tracking = false);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expressions, bool isTracking = false);
    Task<T> FirstOrDefaultAsync(bool isTracking = false);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expressions);
    void Add(T entity);
    void AddRange(IList<T> entites);
    void Update(T entity);
    void UpdateRange(IList<T> entites);
    void Delete(T entity);
    void DeleteRange(IList<T> entites);
    Task SaveAsync();
    void Save();
}
