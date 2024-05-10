using UnitOfWork.Data;
using UnitOfWork.Models;
using UnitOfWork.Services.IRepositories;

namespace UnitOfWork.Services.Repositories;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    public PlayerRepository(DataContext context, ILogger logger) : base(context, logger)
    {
    }
    
    // add methods that are specific to the Player entity
    // e.g Task<Player> GetByEmail(string email);
    // e.g Task<Player> GetByName(string name);
    // e.g Task<Player> GetByEmailAndPassword(string email, string password);
}