

using Abp.Application.Services;
using ParkingSystem.Vehicles.Dto;

namespace ParkingSystem.Vehicles;

public interface IVehicleAppService : IAsyncCrudAppService<VehicleDto,long,PagedVehicleResultRequestDto,CreateVehicleDto,UpdateVehicleDto>
{
}
