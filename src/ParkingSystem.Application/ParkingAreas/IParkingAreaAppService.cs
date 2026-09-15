

using Abp.Application.Services;
using ParkingSystem.ParkingAreas.Dto;

namespace ParkingSystem.ParkingAreas;

public interface IParkingAreaAppService: IAsyncCrudAppService<ParkingAreaDto,long,PagedParkingAreaResultRequestDto,CreateParkingAreaDto,UpdateParkingAreaDto>
{
}
