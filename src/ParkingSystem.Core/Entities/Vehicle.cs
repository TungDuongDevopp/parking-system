

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ParkingSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSystem.Entities;

[Table("Vehicles")]
public class Vehicle : Entity<long>, IHasCreationTime, IHasModificationTime, ISoftDelete

{
    [Required]
    [StringLength(30)]
    public string VehicleCode { get; set; }

    [Required]
    public VehicleType VehicleType { get; set; }

    [StringLength(30)]
    public string? LicensePlate { get; set; }
    
    [StringLength(255)]
    public string? Brand { get; set; }

    [StringLength(255)]
    [Required]
    public string Color { get; set; }

    [Required]
    public long CustomerId { get; set; }

    public Customer Customer { get; set; }
    public DateTime CreationTime { get ; set; }
    public DateTime? LastModificationTime { get ; set; }
    public bool IsDeleted { get; set; }

    public Vehicle()
    {
        CreationTime = Clock.Now;
        IsDeleted = false;
    }
    public ICollection<ParkingSession> ParkingSessions { get; set; } = new List<ParkingSession>();
}
