using Application.Interfaces.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Repositories;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    public PlayerRepository(DataContext context, ILogger<PlayerRepository> logger) : base(context, logger)
    {
    }
    
    // add methods that are specific to the Player entity
    // e.g Task<Player> GetByEmail(string email);
    // e.g Task<Player> GetByName(string name);
    // e.g Task<Player> GetByEmailAndPassword(string email, string password);
}