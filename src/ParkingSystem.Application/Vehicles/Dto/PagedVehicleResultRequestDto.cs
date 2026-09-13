

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;

namespace ParkingSystem.Vehicles.Dto;

public class PagedVehicleResultRequestDto: PagedResultRequestDto, ISortedResultRequest
{
    public string Keyword { get; set; }
    public string Sorting { get; set; }

    public VehicleType? VehicleType { get; set; }

    public long? CustomerId { get; set; }
}
