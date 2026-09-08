using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace ParkingSystem.Web.Views;

public abstract class ParkingSystemRazorPage<TModel> : AbpRazorPage<TModel>
{
    [RazorInject]
    public IAbpSession AbpSession { get; set; }

    protected ParkingSystemRazorPage()
    {
        LocalizationSourceName = ParkingSystemConsts.LocalizationSourceName;
    }
}
