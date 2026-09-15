

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.ParkingAreas.Dto;

public class UpdateParkingAreaDto: EntityDto<long>
{
  
    [StringLength(30)]
    public string ParkingCode { get; set; }

    [StringLength(30)]
    public string Name { get; set; }

    public VehicleType VehicleType { get; set; }

    public int Capacity { get; set; }

    public ParkingMode ParkingMode { get; set; }

    [StringLength(255)]
    public string Location { get; set; }

    [StringLength(255)]
    public string Description { get; set; }
}
