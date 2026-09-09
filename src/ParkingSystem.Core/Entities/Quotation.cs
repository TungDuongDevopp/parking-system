

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ParkingSystem.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSystem.Entities;

[Table("Quotations")]
public class Quotation : Entity<long>, IHasCreationTime, IHasModificationTime


{
    [Required]
    public VehicleType VehicleType { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    public DurationUnit DurationUnit { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Quotation()
    {
        CreationTime = Clock.Now;
    }
}
