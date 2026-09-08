using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ParkingSystem.Configuration;
using ParkingSystem.EntityFrameworkCore;
using ParkingSystem.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace ParkingSystem.Migrator;

[DependsOn(typeof(ParkingSystemEntityFrameworkModule))]
public class ParkingSystemMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public ParkingSystemMigratorModule(ParkingSystemEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(ParkingSystemMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            ParkingSystemConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ParkingSystemMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
