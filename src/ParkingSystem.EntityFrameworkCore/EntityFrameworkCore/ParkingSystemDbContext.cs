using Abp.Zero.EntityFrameworkCore;
using ParkingSystem.Authorization.Roles;
using ParkingSystem.Authorization.Users;
using ParkingSystem.MultiTenancy;
using ParkingSystem.Entities;
using Microsoft.EntityFrameworkCore;

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

    public DbSet<Payment> Payments { get; set; }

    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

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

        //Payment entity configuration
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Subscription)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.SubscriptionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        //PaymentTransaction entity configuration
        modelBuilder.Entity<PaymentTransaction>()
            .HasOne(pt => pt.Payment)
            .WithMany(p => p.PaymentTransactions)
            .HasForeignKey(pt => pt.PaymentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PaymentTransaction>()
            .HasIndex(pt => pt.TransactionCode)
            .IsUnique();
    }
}
