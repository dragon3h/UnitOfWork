using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected DataContext _context;
    private DbSet<T?> dbSet;
    private readonly ILogger _logger;
    
    public GenericRepository(DataContext context, ILogger logger)
    {
        _context = context;
        this.dbSet = context.Set<T>();
        _logger = logger;
    }
    
    public virtual async Task<IEnumerable<T?>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        try
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetById method with id: {id}", id);
            return null;
        }
    }

    public virtual async Task<T?> Add(T? entity)
    {
        try
        {
            await dbSet.AddAsync(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Add method with entity: {entity}", entity);
            return null;
        }
    }

    public virtual async Task<T?> Update(T? entity)
    {
        var player = await dbSet.FirstOrDefaultAsync(e => e!.Id == entity!.Id);
        if (player != null)
        {
            dbSet.Update(entity);
            return entity;
        }
        else
        {
            _logger.LogError("Entity not found");
            return null;
        }
    }

    public virtual async Task<bool> Delete(int id)
    {
        try
        {
            var entity = await dbSet.FindAsync(id);
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