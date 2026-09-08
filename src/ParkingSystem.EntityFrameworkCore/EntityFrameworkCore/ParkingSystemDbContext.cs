using Abp.Zero.EntityFrameworkCore;
using ParkingSystem.Authorization.Roles;
using ParkingSystem.Authorization.Users;
using ParkingSystem.MultiTenancy;
using ParkingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParkingSystem.EntityFrameworkCore;

public class ParkingSystemDbContext : AbpZeroDbContext<Tenant, Role, User, ParkingSystemDbContext>
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Staff> Staffs { get; set; }

    public ParkingSystemDbContext(DbContextOptions<ParkingSystemDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Customer entity configuration
        modelBuilder.Entity<Customer>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Customer>(c => c.UserId);
       
        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.UserId)
            .IsUnique();

        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();
        modelBuilder.Entity<Customer>()
            .HasIndex(x => x.Email)
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");

        //Staff entity configuration
        modelBuilder.Entity<Staff>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Staff>(s => s.UserId);
        modelBuilder.Entity<Staff>()
            .HasIndex(x => x.UserId)
            .IsUnique();

        modelBuilder.Entity<Staff>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();
        modelBuilder.Entity<Staff>()
            .HasIndex(x => x.Email)
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");
    }
}
