using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ParkingSystem.EntityFrameworkCore;

public static class ParkingSystemDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<ParkingSystemDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<ParkingSystemDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
