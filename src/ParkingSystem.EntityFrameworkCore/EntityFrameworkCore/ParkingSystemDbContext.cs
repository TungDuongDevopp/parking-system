using Abp.Zero.EntityFrameworkCore;
using ParkingSystem.Authorization.Roles;
using ParkingSystem.Authorization.Users;
using ParkingSystem.MultiTenancy;
using ParkingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace ParkingSystem.EntityFrameworkCore;

public class ParkingSystemDbContext(DbContextOptions<ParkingSystemDbContext> options) : AbpZeroDbContext<Tenant, Role, User, ParkingSystemDbContext>(options)
{
    /* Define a DbSet for each entity of the application */
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<ParkingArea> ParkingAreas { get; set; }

    public DbSet<ParkingSpot> ParkingSpots { get; set; }

    public DbSet<Quotation> Quotations { get; set; }

    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<ParkingSession> ParkingSessions { get; set; }

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

        //Vehicle entity configuration
        modelBuilder.Entity<Vehicle>()
        
                .HasOne(v => v.Customer)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
     
        modelBuilder.Entity<Vehicle>()
            .HasIndex(v => v.VehicleCode)
            .IsUnique();

        //ParkingArea entity configuration
        modelBuilder.Entity<ParkingArea>()
            .HasIndex(p => p.ParkingCode)
            .IsUnique();

        //ParkingSpot entity configuration
        modelBuilder.Entity<ParkingSpot>()
             .HasIndex(s => new { s.ParkingAreaId, s.SpotCode })
             .IsUnique();

        modelBuilder.Entity<ParkingSpot>()
            .HasOne(p => p.ParkingArea)
            .WithMany(pa => pa.ParkingSpots)
            .HasForeignKey(p => p.ParkingAreaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


        //Quotation entity configuration
        modelBuilder.Entity<Quotation>()
            .HasIndex(q => new { q.VehicleType, q.Duration, q.DurationUnit })
            .IsUnique();
        //Subscription entity configuration

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Customer)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(s => s.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Quotation)
            .WithMany()
            .HasForeignKey(s => s.QuotationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        //ParkingSession entity configuration
        modelBuilder.Entity<ParkingSession>()
            .HasOne(ps => ps.ParkingSpot)
            .WithMany()
            .HasForeignKey(ps => ps.ParkingSpotId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParkingSession>()
            .HasOne(ps => ps.Vehicle)
            .WithMany(v => v.ParkingSessions)
            .HasForeignKey(ps => ps.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParkingSession>()
            .HasOne(ps => ps.Quotation)
            .WithMany()
            .HasForeignKey(ps => ps.QuotationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        // Apply global query filter for entities implementing ISoftDelete
        var softDeleteInterface = typeof(Abp.Domain.Entities.ISoftDelete);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(t => softDeleteInterface.IsAssignableFrom(t.ClrType)))
        {
            var clrType = entityType.ClrType;
            var parameter = Expression.Parameter(clrType, "e");
            var efPropertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Public | BindingFlags.Static)?.MakeGenericMethod(typeof(bool));
            var isDeletedProperty = Expression.Call(efPropertyMethod!, parameter, Expression.Constant("IsDeleted"));
            var condition = Expression.Equal(isDeletedProperty, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parameter);
            modelBuilder.Entity(clrType).HasQueryFilter(lambda);
        }

    }
}
