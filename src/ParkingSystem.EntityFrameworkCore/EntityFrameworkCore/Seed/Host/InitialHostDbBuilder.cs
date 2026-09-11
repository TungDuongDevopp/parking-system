using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ParkingSystem.EntityFrameworkCore.Seed.Host;

public class InitialHostDbBuilder
{
    private readonly ParkingSystemDbContext _context;

    public InitialHostDbBuilder(ParkingSystemDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();

        // Ensure technical default tenant exists even when multi-tenancy is disabled.
        new Tenants.DefaultTenantBuilder(_context).Create();

        // Create tenant roles and users for the technical default tenant.
        var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == Abp.MultiTenancy.AbpTenantBase.DefaultTenantName);
        if (defaultTenant != null)
        {
            new Tenants.TenantRoleAndUserBuilder(_context, defaultTenant.Id).Create();
        }

        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context).Create();

        _context.SaveChanges();
    }
}
