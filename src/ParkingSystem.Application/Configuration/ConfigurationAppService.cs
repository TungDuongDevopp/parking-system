using Abp.Authorization;
using Abp.Runtime.Session;
using ParkingSystem.Configuration.Dto;
using System.Threading.Tasks;

namespace ParkingSystem.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : ParkingSystemAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
