using ParkingSystem.Roles.Dto;
using System.Collections.Generic;

namespace ParkingSystem.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}