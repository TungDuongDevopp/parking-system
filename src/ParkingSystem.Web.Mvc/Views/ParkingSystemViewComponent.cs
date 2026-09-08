using Abp.AspNetCore.Mvc.ViewComponents;

namespace ParkingSystem.Web.Views;

public abstract class ParkingSystemViewComponent : AbpViewComponent
{
    protected ParkingSystemViewComponent()
    {
        LocalizationSourceName = ParkingSystemConsts.LocalizationSourceName;
    }
}
