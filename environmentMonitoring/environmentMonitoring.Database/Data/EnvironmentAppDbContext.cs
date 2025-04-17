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

            modelBuilder.Entity<RolePermissions>()
            .HasKey(rp => new { rp.role_Id, rp.permission_Id });


            modelBuilder.Entity<Role>().HasData(
                new Role { role_Id = 1, role_type = "Admin" },
                new Role { role_Id = 2, role_type = "Environment Scientist" },
                new Role { role_Id = 3, role_type = "Operations Manager" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { permission_Id = 1, name = "CreateUsers", description = "create a new user account" },
                new Permission { permission_Id = 2, name = "ReadUsers", description = "Read a users account details" },
                new Permission { permission_Id = 3, name = "UpdateUsers", description = "Update a users account details" },
                new Permission { permission_Id = 4, name = "DeleteUsers", description = "Delete a users account" },
                new Permission { permission_Id = 5, name = "CreateSensors", description = "create a new sensor account" },
                new Permission { permission_Id = 6, name = "ReadSensors", description = "Read a sensors account details" },
                new Permission { permission_Id = 7, name = "UpdateSensors", description = "Update a sensors account details" },
                new Permission { permission_Id = 8, name = "DeleteSensors", description = "Delete a sensors account" },
                new Permission { permission_Id = 9, name = "ManageUserRoles", description = "assign roles to users" },
                new Permission { permission_Id = 10, name = "SetRolePermissions", description = "Set role permissions" },
                new Permission { permission_Id = 11, name = "CreateIncidentReport", description = "Create incident report" },
                new Permission { permission_Id = 12, name = "ReadIncidentReport", description = "Read incident report" },
                new Permission { permission_Id = 13, name = "UpdateIncidentReport", description = "Update incident report" },
                new Permission { permission_Id = 14, name = "DeleteIncidentReport", description = "Delete incident report" }
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
