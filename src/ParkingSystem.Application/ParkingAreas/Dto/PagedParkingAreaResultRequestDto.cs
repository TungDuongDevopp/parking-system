

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;

namespace ParkingSystem.ParkingAreas.Dto;

public class PagedParkingAreaResultRequestDto: PagedResultRequestDto, ISortedResultRequest
{
    public string Keyword { get; set; }
    public string Sorting { get; set; }

    public VehicleType? VehicleType { get; set; }

    public int? MinCapacity { get; set; }

    public int? MaxCapacity { get; set; }

    public ParkingMode? ParkingMode { get; set; }
}
