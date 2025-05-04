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
        var connectionString = Environment.GetEnvironmentVariable($"ConnectionStrings__{"DevelopmentConnection"}");

         if (string.IsNullOrEmpty(connectionString))
         {
             var assembly = Assembly.GetExecutingAssembly();
             using var stream = assembly.GetManifestResourceStream("environmentMonitoring.Database.appsettings.json");

             if (stream != null)
             {
                 var config = new ConfigurationBuilder()
                     .AddJsonStream(stream)
                     .Build();

                 connectionString = config.GetConnectionString("DevelopmentConnection");
             }
         }

         if (string.IsNullOrEmpty(connectionString))
         {
             throw new InvalidOperationException("Database connection string is not configured.");
         }

         optionsBuilder.UseSqlServer(
             connectionString,
             m => m.MigrationsAssembly("environmentMonitoring.Migrations"));


    /*
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
    */
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
    public DbSet<RolePermissions> RolePermissions { get; set; }

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

        modelBuilder.Entity<User>()
            .HasKey(e => e.user_Id);

        modelBuilder.Entity<RealSensor>()
            .HasKey(e => e.r_sensor_Id);

        modelBuilder.Entity<VirtualSensor>()
            .HasKey(e => e.v_sensor_Id);

        modelBuilder.Entity<Quantities>()
            .HasKey(e => e.quantity_Id);

        modelBuilder.Entity<IncidentReports>()
            .HasKey(e => e.incident_Id);

        modelBuilder.Entity<Reports>()
            .HasKey(e => e.report_Id);

        modelBuilder.Entity<Permission>()
            .HasKey(e => e.permission_Id);

        modelBuilder.Entity<Role>()
            .HasKey(e => e.role_Id);

        modelBuilder.Entity<Readings>()
            .HasKey(e => e.reading_Id);

        modelBuilder.Entity<RolePermissions>()
            .HasKey(rp => new { rp.role_Id, rp.permission_Id });


            modelBuilder.Entity<Role>().HasData(
                new Role { role_Id = 1, role_type = "Admin" },
                new Role { role_Id = 2, role_type = "Environment Scientist" },
                new Role { role_Id = 3, role_type = "Operations Manager" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { permission_Id = 1, name = "CreateUsers", description = "create user" },
                new Permission { permission_Id = 2, name = "ReadUsers", description = "read users" },
                new Permission { permission_Id = 3, name = "UpdateUsers", description = "update users" },
                new Permission { permission_Id = 4, name = "DeleteUsers", description = "delete users" },
                new Permission { permission_Id = 5, name = "CreateSensors", description = "create sensor" },
                new Permission { permission_Id = 6, name = "ReadSensors", description = "read sensors" },
                new Permission { permission_Id = 7, name = "UpdateSensors", description = "update sensors" },
                new Permission { permission_Id = 8, name = "DeleteSensors", description = "delete sensor" },
                new Permission { permission_Id = 9, name = "ManageUserRoles", description = "assign roles" },
                new Permission { permission_Id = 10, name = "SetRolePermissions", description = "set role permissions" },
                new Permission { permission_Id = 11, name = "CreateIncidentReport", description = "create incident report" },
                new Permission { permission_Id = 12, name = "ReadIncidentReport", description = "read incident report" },
                new Permission { permission_Id = 13, name = "UpdateIncidentReport", description = "update incident report" },
                new Permission { permission_Id = 14, name = "DeleteIncidentReport", description = "delete incident report" }
            );

             modelBuilder.Entity<RolePermissions>().HasData(
                new RolePermissions { role_Id = 1, permission_Id = 1 },
                new RolePermissions { role_Id = 1, permission_Id = 2 },
                new RolePermissions { role_Id = 1, permission_Id = 3 },
                new RolePermissions { role_Id = 1, permission_Id = 4 },
                new RolePermissions { role_Id = 1, permission_Id = 5 },
                new RolePermissions { role_Id = 1, permission_Id = 6 },
                new RolePermissions { role_Id = 1, permission_Id = 7 },
                new RolePermissions { role_Id = 1, permission_Id = 8 },
                new RolePermissions { role_Id = 1, permission_Id = 9 },
                new RolePermissions { role_Id = 1, permission_Id = 10 },
                new RolePermissions { role_Id = 1, permission_Id = 11 },
                new RolePermissions { role_Id = 1, permission_Id = 12 },
                new RolePermissions { role_Id = 1, permission_Id = 13 },
                new RolePermissions { role_Id = 1, permission_Id = 14 },
                new RolePermissions { role_Id = 2, permission_Id = 2 },
                new RolePermissions { role_Id = 2, permission_Id = 5 },
                new RolePermissions { role_Id = 2, permission_Id = 6 },
                new RolePermissions { role_Id = 2, permission_Id = 7 },
                new RolePermissions { role_Id = 2, permission_Id = 8 },
                new RolePermissions { role_Id = 2, permission_Id = 11 },
                new RolePermissions { role_Id = 2, permission_Id = 12 },
                new RolePermissions { role_Id = 2, permission_Id = 13 },
                new RolePermissions { role_Id = 2, permission_Id = 14 },
                new RolePermissions { role_Id = 3, permission_Id = 2 },
                new RolePermissions { role_Id = 3, permission_Id = 6 },
                new RolePermissions { role_Id = 3, permission_Id = 11 }
            );


    }

    

}
