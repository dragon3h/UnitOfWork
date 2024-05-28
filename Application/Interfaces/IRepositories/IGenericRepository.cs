using Domain.Models;

namespace Application.Interfaces.IRepositories;

public interface IGenericRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> Add(T entity);
    Task<T?> Update(T entity);
    Task<bool> Delete(int id);
}
