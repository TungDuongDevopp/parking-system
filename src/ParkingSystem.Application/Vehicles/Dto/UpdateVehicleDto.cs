

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Vehicles.Dto;

[AutoMap(typeof(Vehicle))]
public class UpdateVehicleDto: EntityDto<long>
{

    public VehicleType VehicleType { get; set; }

    [StringLength(30)]
    public string? LicensePlate { get; set; }

    [StringLength(255)]
    public string? Brand { get; set; }

    [StringLength(255)]
    public string Color { get; set; }
}
