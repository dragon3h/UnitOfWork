using Microsoft.EntityFrameworkCore;
using UnitOfWork.Models;

namespace UnitOfWork.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    // DbSets are used to query and save instances of entities to a database
    // Here the DbSet is of type Player and the name of the table in the database will be Players
    public DbSet<Player> Players { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }
}