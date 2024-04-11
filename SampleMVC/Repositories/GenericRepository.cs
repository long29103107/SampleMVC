
using Microsoft.EntityFrameworkCore;
using SampleMVC.Data;
using System.Linq.Expressions;

namespace SampleMVC.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private IQueryable<T> InternalFilter(Expression<Func<T, bool>>? expression = null)
    {
        if(expression == null)
        {
            return _context.Set<T>();
        }
        return _context.Set<T>().Where(expression);
    }

    public IQueryable<T> FindAll(bool tracking = false)
    {
        if(tracking)
        {
            return InternalFilter();
        }
        return InternalFilter().AsNoTracking();
    }

    public IQueryable FindByCondition(Expression<Func<T, bool>> expressions = null, bool tracking = false)
    {
        if (tracking)
        {
            return InternalFilter().Where(expressions);
        }
        return InternalFilter().AsNoTracking().Where(expressions);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expressions, bool isTracking = false)
    {
        if (isTracking)
        {
            return await this.InternalFilter().FirstOrDefaultAsync(expressions);
        }
        return await this.InternalFilter().AsNoTracking().FirstOrDefaultAsync(expressions);
    }

    public async Task<T> FirstOrDefaultAsync(bool isTracking = false)
    {
        if (isTracking)
        {
            return await this.InternalFilter().FirstOrDefaultAsync();
        }
        return await this.InternalFilter().AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expressions)
    {
       return await this.InternalFilter().AnyAsync(expressions);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IList<T> entites)
    {
        _context.Set<T>().AddRange(entites);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IList<T> entites)
    {
        _context.Set<T>().RemoveRange(entites);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void UpdateRange(IList<T> entites)
    {
        _context.Set<T>().UpdateRange();
    }
}
