

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;


namespace ParkingSystem.ParkingSpots.Dto;
public class ParkingSpotDto: EntityDto<long>
{
    public string SpotCode { get; set; }

    public ParkingSpotStatus Status { get; set; }

    public long ParkingAreaId { get; set; }

}
