

using Abp.Application.Services;
using ParkingSystem.Staffs.Dto;

namespace ParkingSystem.Staffs;

public interface IStaffAppService : IAsyncCrudAppService<StaffDto,long,PagedStaffResultRequestDto,CreateStaffDto,UpdateStaffDto>
{
}
