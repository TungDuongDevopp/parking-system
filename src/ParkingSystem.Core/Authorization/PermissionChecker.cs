using Abp.Authorization;
using ParkingSystem.Authorization.Roles;
using ParkingSystem.Authorization.Users;

namespace ParkingSystem.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
