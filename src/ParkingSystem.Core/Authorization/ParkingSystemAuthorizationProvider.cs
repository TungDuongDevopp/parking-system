using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ParkingSystem.Authorization;

/// <summary>
/// This class singlely defines the permissions for the application.
/// </summary>
public class ParkingSystemAuthorizationProvider : AuthorizationProvider
{

    public override void SetPermissions(IPermissionDefinitionContext context)
    {
        context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
        context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
        context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
        context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        //Customer
        context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"));
        context.CreatePermission(PermissionNames.Pages_Customers_ViewAll, L("Customers_ViewAll"));
        context.CreatePermission(PermissionNames.Pages_Customers_ModifyAll, L("Customers_ModifyAll"));
        //Staff
        context.CreatePermission(PermissionNames.Pages_Staffs, L("Staffs"));
        context.CreatePermission(PermissionNames.Pages_Staffs_Manager, L("Staffs_Manager"));
        //Vehicle
        context.CreatePermission(PermissionNames.Pages_Vehicles, L("Vehicles"));
        context.CreatePermission(PermissionNames.Pages_Vehicles_ModifyAll, L("Vehicles_ModifyAll"));
        context.CreatePermission(PermissionNames.Pages_Vehicles_ViewAll, L("Vehicles_ViewAll"));
        

    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, ParkingSystemConsts.LocalizationSourceName);
    }
}
