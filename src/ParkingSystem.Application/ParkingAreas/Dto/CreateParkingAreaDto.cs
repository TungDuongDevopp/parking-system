
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.ParkingAreas.Dto;

public class CreateParkingAreaDto
{

    [Required]
    [StringLength(30)]
    public string ParkingCode { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    public VehicleType VehicleType { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public ParkingMode ParkingMode { get; set; }

    [Required]
    [StringLength(255)]
    public string Location { get; set; }

    [StringLength(255)]
    public string Description { get; set; }
}
