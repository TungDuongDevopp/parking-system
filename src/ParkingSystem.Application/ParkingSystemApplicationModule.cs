using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ParkingSystem.Authorization;

namespace ParkingSystem;

[DependsOn(
    typeof(ParkingSystemCoreModule),
    typeof(AbpAutoMapperModule))]
public class ParkingSystemApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<ParkingSystemAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(ParkingSystemApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
