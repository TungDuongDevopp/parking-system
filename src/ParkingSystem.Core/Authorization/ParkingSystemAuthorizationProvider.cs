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
        context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"));

    }

    private static ILocalizableString L(string name)
    {
        return new LocalizableString(name, ParkingSystemConsts.LocalizationSourceName);
    }
}
