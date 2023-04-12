using Microsoft.EntityFrameworkCore;
using SherpaBackEnd.Dtos;

namespace SherpaBackEnd.Helpers;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseInMemoryDatabase("TestDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>().HasData(
            new Group("Group A"), new Group("Group B"));
    }

    public DbSet<Group> Groups { get; set; }
}
