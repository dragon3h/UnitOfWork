using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data;
using UnitOfWork.Services.IRepositories;

namespace UnitOfWork.Services.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DataContext _context;
    protected DbSet<T> dbSet;
    protected readonly ILogger _logger;
    
    public GenericRepository(DataContext context, ILogger logger)
    {
        _context = context;
        this.dbSet = context.Set<T>();
        _logger = logger;
    }
    
    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        try
        {
            return await dbSet.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetById method with id: {id}", id);
            return null;
        }
    }

    public virtual async Task<bool> Add(T entity)
    {
        try
        {
            await dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Add method with entity: {entity}", entity);
            return false;
        }
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> Delete(int id)
    {
        try
        {
            var entity = dbSet.Find(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                return true;
            }
            else
            {
                _logger.LogError("Entity with id: {id} not found", id);
                return false;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Delete method with id: {id}", id);
            return false;
        }
    }
}