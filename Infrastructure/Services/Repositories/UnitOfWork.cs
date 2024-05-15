using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ILoggerFactory _loggerFactory;
    public IPlayerRepository Players { get; private set; }
    
    public UnitOfWork(DataContext context, ILogger<UnitOfWork> logger, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = logger;
        _loggerFactory = loggerFactory;
        Players = new PlayerRepository(context, _loggerFactory.CreateLogger<PlayerRepository>());
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