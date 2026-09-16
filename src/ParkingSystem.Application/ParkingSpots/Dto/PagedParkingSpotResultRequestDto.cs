

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;

namespace ParkingSystem.ParkingSpots.Dto;

public class PagedParkingSpotResultRequestDto: PagedResultRequestDto, ISortedResultRequest
{
    public string Keyword { get; set; }
    public string Sorting { get; set; }

    public ParkingSpotStatus? Status { get; set; }

    public long? ParkingAreaId { get; set; }
}
