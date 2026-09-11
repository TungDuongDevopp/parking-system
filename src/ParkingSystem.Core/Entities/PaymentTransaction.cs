

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ParkingSystem.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Entities;

public class PaymentTransaction : Entity<long>, IHasCreationTime, IHasModificationTime
{
    [Required]
    [StringLength(255)]
    public string TransactionCode { get; set; }
    [Required]
    public decimal Amount { get; set; }

    [Required]
    public TransactionStatus Status { get; set; }
    
    [Required]
    public TransactionType Type { get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public DateTime? TransactionTime { get; set; }

    public Payment Payment { get; set; }

    [Required]
    public long PaymentId { get; set; }
    public PaymentTransaction()
    {
        CreationTime = DateTime.Now;
        Status = TransactionStatus.Pending;
    }
}

