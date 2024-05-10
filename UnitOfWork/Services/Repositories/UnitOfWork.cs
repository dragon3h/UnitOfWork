using UnitOfWork.Data;
using UnitOfWork.Services.IRepositories;

namespace UnitOfWork.Services.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private readonly ILogger _logger;
    
    public IPlayerRepository Players { get; private set; }
    
    public UnitOfWork(DataContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
        Players = new PlayerRepository(context, logger);
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}