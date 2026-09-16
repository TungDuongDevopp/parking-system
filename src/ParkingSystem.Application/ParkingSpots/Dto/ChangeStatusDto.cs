
using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;


namespace ParkingSystem.ParkingSpots.Dto;

public class ChangeStatusDto:EntityDto<long>
{
    [EnumDataType(typeof(ParkingSpotStatus))]
    public ParkingSpotStatus Status { get; set; }
}
