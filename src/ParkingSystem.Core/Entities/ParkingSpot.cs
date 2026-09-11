
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Entities;

public class ParkingSpot : Entity<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
{

    [Required]
    [StringLength(30)]
    public string SpotCode { get; set; }

    [Required]
    public bool IsFree { get; set; }
    public DateTime CreationTime { get ; set; }
    public DateTime? LastModificationTime { get ; set; }

    public long ParkingAreaId { get; set; }

    public ParkingArea ParkingArea{ get; set; }

    public ParkingSpot()
    {
        CreationTime = Clock.Now;
        IsFree = true;
        IsDeleted = false;
    }
    public bool IsDeleted { get; set; }
}
