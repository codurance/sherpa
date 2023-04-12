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
        modelBuilder.Entity<GroupDTO>().HasData(
            new GroupDTO("Group A"), new GroupDTO("Group B"));
    }

    public DbSet<GroupDTO> Groups { get; set; }
}
