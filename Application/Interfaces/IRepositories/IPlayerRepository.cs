using Domain.Models;

namespace Application.Interfaces.IRepositories;

public interface IPlayerRepository : IGenericRepository<Player>
{
    // add methods that are specific to the Player entity
    // e.g Task<Player> GetByEmail(string email);
    // e.g Task<Player> GetByName(string name);
    // e.g Task<Player> GetByEmailAndPassword(string email, string password);
}
