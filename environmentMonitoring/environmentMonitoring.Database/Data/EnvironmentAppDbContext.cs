using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using environmentMonitoring.Database.Models;

namespace environmentMonitoring.Database.Data;

public class EnvironmentAppDbContext : DbContext
{
     public EnvironmentAppDbContext()
    { }
    public EnvironmentAppDbContext(DbContextOptions options) : base(options)
    { }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
        var a = Assembly.GetExecutingAssembly();
        var resources = a.GetManifestResourceNames();
        using var stream = a.GetManifestResourceStream("Notes.Database.appsettings.json");
        
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        
        optionsBuilder.UseSqlServer(
            config.GetConnectionString("DevelopmentConnection")
        );
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


}
