
using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;


namespace ParkingSystem.ParkingAreas.Dto;

public class ChangeStatusDto:EntityDto<long>
{
    [Required]
    public ParkingSpotStatus Status { get; set; }
}
