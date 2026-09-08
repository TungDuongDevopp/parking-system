using ParkingSystem.Roles.Dto;
using System.Collections.Generic;

namespace ParkingSystem.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
