using ParkingSystem.Configuration.Dto;
using System.Threading.Tasks;

namespace ParkingSystem.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
