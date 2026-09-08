using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using ParkingSystem.Authorization;

namespace ParkingSystem.Web.Startup;

/// <summary>
/// This class defines menus for the application.
/// </summary>
public class ParkingSystemNavigationProvider : NavigationProvider
{
    public override void SetNavigation(INavigationProviderContext context)
    {
        context.Manager.MainMenu
            .AddItem(
                new MenuItemDefinition(
                    PageNames.Home,
                    L("HomePage"),
                    url: "",
                    icon: "fas fa-home",
                    requiresAuthentication: true
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Tenants,
                    L("Tenants"),
                    url: "Tenants",
                    icon: "fas fa-building",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Users,
                    L("Users"),
                    url: "Users",
                    icon: "fas fa-users",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Roles,
                    L("Roles"),
                    url: "Roles",
                    icon: "fas fa-theater-masks",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                )
            ).AddItem(
                new MenuItemDefinition(
                    PageNames.Customers,
                    L("Customers"),
                    url: "Customer",
                    icon: "fas fa-address-book",
                    permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Customers)
    )
)
            ;

    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, ParkingSystemConsts.LocalizationSourceName);
    }
}