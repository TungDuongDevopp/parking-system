

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ParkingSystem.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Entities;

public class ParkingSession : Entity<long>, IHasCreationTime, IHasModificationTime
{
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    [Required]
    public bool IsPaid { get; set; }

    public decimal Fee { get; set; }
    
   
    public PaymentMethod? PaymentMethod { get; set; }

    [Required]
    public long ParkingSpotId { get; set; }

    public ParkingSpot ParkingSpot { get; set; }

    public long? VehicleId { get; set; }

    public Vehicle Vehicle { get; set; }

    public Quotation Quotation { get; set; }

    [Required]
    public long QuotationId { get; set; }

    public string? PlateNumber { get; set; }

    [Required]
    public string EntryImageUrl { get; set; }
    public string? ExitImageUrl { get; set; }

    [Required]
    public  DateTime EntryTime { get; set; }

    public DateTime? ExitTime { get; set; }

    public ParkingSession()
    {
        CreationTime = Clock.Now;
        EntryTime = Clock.Now;
        IsPaid = false;
    }

}
