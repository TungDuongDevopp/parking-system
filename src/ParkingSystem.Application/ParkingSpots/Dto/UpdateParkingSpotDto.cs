

using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.ParkingSpots.Dto;

public class UpdateParkingSpotDto: EntityDto<long>
{
    [StringLength(30)]
    public string? SpotCode { get; set; }

}
