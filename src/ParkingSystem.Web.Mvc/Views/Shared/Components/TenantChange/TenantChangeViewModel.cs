using Abp.AutoMapper;
using ParkingSystem.Sessions.Dto;

namespace ParkingSystem.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
