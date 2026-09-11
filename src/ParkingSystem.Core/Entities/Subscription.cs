
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using ParkingSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingSystem.Entities;

[Table("Subscriptions")]
public class Subscription: Entity<long>, IHasCreationTime, IHasModificationTime


{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Required]
    public SubcriptionStatus Status { get; set; }

    public Quotation Quotation { get; set; }
  
    [Required]
    public long QuotationId { get; set; }

    public Customer Customer { get; set; }
    [Required]
    public long CustomerId { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public Subscription()
    {
        CreationTime = Clock.Now;
        Status = SubcriptionStatus.PENDING;
    }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
