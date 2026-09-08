using Abp.Modules;
using Abp.Reflection.Extensions;
using ParkingSystem.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ParkingSystem.Web.Startup;

[DependsOn(typeof(ParkingSystemWebCoreModule))]
public class ParkingSystemWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public ParkingSystemWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<ParkingSystemNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(ParkingSystemWebMvcModule).GetAssembly());
    }
}
