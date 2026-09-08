using ParkingSystem.Roles.Dto;
using System.Collections.Generic;

namespace ParkingSystem.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
