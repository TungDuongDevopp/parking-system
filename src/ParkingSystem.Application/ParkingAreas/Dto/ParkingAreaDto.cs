

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System;

namespace ParkingSystem.ParkingAreas.Dto;

public class ParkingAreaDto : EntityDto<long>
{
    public string ParkingCode { get; set; }

    public string Name { get; set; }

    public VehicleType VehicleType { get; set; }

    public string VehicleTypeName => VehicleType.ToString();
    
    public int Capacity { get; set; }

    public ParkingMode ParkingMode { get; set; }

    public string ParkingModeName => ParkingCode.ToString();

    public string Location { get; set; }

    public string Description { get; set; }

    public DateTime CreationTime { get; set; }

    public bool IsDeleted { get; set; }

}
