using Abp.AutoMapper;
using ParkingSystem.Roles.Dto;
using ParkingSystem.Web.Models.Common;

namespace ParkingSystem.Web.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}
