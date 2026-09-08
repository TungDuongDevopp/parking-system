using Abp.Application.Services;
using ParkingSystem.MultiTenancy.Dto;

namespace ParkingSystem.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

