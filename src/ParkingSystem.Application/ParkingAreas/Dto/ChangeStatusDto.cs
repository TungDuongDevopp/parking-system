

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.ParkingAreas.Dto;

public class ChangeStatusDto:EntityDto<long>
{
    [EnumDataType(typeof(ParkingAreaStatus))]
    public ParkingAreaStatus Status { get; set; }
}
