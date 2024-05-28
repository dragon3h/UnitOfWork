using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected DataContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly ILogger _logger;

    public GenericRepository(DataContext context, ILogger logger)
    {
        _context = context;
        this._dbSet = context.Set<T>();
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetById method with id: {id}", id);
            throw new Exception("Error in GetById, {e}", e);
        }
    }

    public virtual async Task<T> Add(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Add method with entity: {entity}", entity);
            throw new Exception("Error in Add, {e}", e);
        }
    }

    public virtual async Task<T> Update(T entity)
    {
        var player = await _dbSet.FirstOrDefaultAsync(e => e!.Id == entity!.Id);
        if (player != null)
        {
            _dbSet.Update(entity);
            return entity;
        }
        else
        {
            _logger.LogError("Entity not found");
            throw new Exception("Entity not found");
        }
    }

    public virtual async Task<bool> Delete(int id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
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
            throw new Exception("Error in Delete method, {e}", e);
        }
    }
}