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
        using var stream = a.GetManifestResourceStream("environmentMonitoring.Database.appsettings.json");
        
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        
        optionsBuilder.UseSqlServer(
            config.GetConnectionString("DevelopmentConnection"),
            m => m.MigrationsAssembly("environmentMonitoring.Migrations")
        );
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<IncidentReports> IncidentReports { get; set; }
    public DbSet<Quantities> Quantities { get; set; }
    public DbSet<Readings> Readings { get; set; }
    public DbSet<RealSensor> RealSensors { get; set; }
    public DbSet<Reports> Reports { get; set; }
    public DbSet<VirtualSensor> VirtualSensors { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(e => e.Role)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.role_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VirtualSensor>()
            .HasOne(e => e.RealSensor)
            .WithMany(e => e.VirtualSensor)
            .HasForeignKey(e => e.r_sensor_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VirtualSensor>()
            .HasOne(e => e.Quantity)
            .WithMany(e => e.VirtualSensor)
            .HasForeignKey(e => e.quantity_id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Readings>()
            .HasOne(e => e.VirtualSensor)
            .WithMany(e => e.Readings)
            .HasForeignKey(e => e.v_sensor_id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IncidentReports>()
            .HasOne(e => e.Reading)
            .WithMany(e => e.IncidentReports)
            .HasForeignKey(e => e.reading_Id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reports>()
            .HasOne(e => e.VirtualSensor)
            .WithMany(e => e.Reports)
            .HasForeignKey(e => e.v_sensor_id)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermissions>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.role_Id);

            modelBuilder.Entity<RolePermissions>()
            .HasOne(rp => rp.Permissions)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.permission_Id);

            modelBuilder.Entity<RolePermissions>()
            .HasKey(rp => new { rp.role_Id, rp.permission_Id });


    }

   

}
