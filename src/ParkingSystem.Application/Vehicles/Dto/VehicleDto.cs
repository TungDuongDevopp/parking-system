

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using ParkingSystem.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Vehicles.Dto;

[AutoMapFrom(typeof(Vehicle))]
public class VehicleDto : EntityDto<long>
{

    public string VehicleCode { get; set; }

    public VehicleType VehicleType { get; set; }

    public string VehicleTypename => VehicleType.ToString();
    public string LicensePlate { get; set; }

    public string Brand { get; set; }
    public string Color { get; set; }
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }

    public long CustomerId { get; set;}
}
