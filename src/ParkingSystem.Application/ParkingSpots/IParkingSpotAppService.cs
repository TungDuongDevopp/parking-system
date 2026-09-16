
using Abp.Application.Services;
using ParkingSystem.ParkingAreas.Dto;
using ParkingSystem.ParkingSpots.Dto;
using System.Threading.Tasks;

namespace ParkingSystem.ParkingSpots;
public interface IParkingSpotAppService : IAsyncCrudAppService<ParkingSpotDto,long,PagedParkingSpotResultRequestDto,CreateParkingSpotDto,UpdateParkingSpotDto>
{
     Task ChangeStatus(ChangeStatusDto dto);
}
