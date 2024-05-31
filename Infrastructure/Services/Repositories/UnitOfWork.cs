using Application.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DataContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private IDbContextTransaction _transaction;
    public IPlayerRepository Players { get; private set; }

    public UnitOfWork(DataContext context, ILogger<UnitOfWork> logger, ILoggerFactory loggerFactory)
    {
        _context = context;
        _logger = logger;
        _loggerFactory = loggerFactory;
        Players = new PlayerRepository(context, _loggerFactory.CreateLogger<PlayerRepository>());
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        _context.Dispose();
    }
}