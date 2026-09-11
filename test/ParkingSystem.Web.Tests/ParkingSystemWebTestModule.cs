using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ParkingSystem.EntityFrameworkCore;
using ParkingSystem.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace ParkingSystem.Web.Tests;

[DependsOn(
    typeof(ParkingSystemWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class ParkingSystemWebTestModule : AbpModule
{
    public ParkingSystemWebTestModule(ParkingSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ParkingSystemWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        var partManager = IocManager.Resolve<ApplicationPartManager>();
        partManager.AddApplicationPartsIfNotAddedBefore(typeof(ParkingSystemWebMvcModule).Assembly);
        partManager.AddApplicationPartsIfNotAddedBefore(typeof(ParkingSystemWebTestModule).Assembly);
    }
}