

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using ParkingSystem.Validation;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.ParkingAreas.Dto;

public class UpdateParkingAreaDto: EntityDto<long>
{
  
    [StringLength(30)]
    public string? ParkingCode { get; set; }

    [StringLength(30)]
    public string? Name { get; set; }
   
    [EnumDataType(typeof(VehicleType))]
    public VehicleType? VehicleType { get; set; }

    [GreaterThanZero]
    public int? Capacity { get; set; }

    [EnumDataType(typeof(ParkingMode))]
    public ParkingMode ParkingMode { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }
}
