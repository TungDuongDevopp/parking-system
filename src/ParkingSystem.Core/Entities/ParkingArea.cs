

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Entities;

public class ParkingArea : Entity<long>, IHasCreationTime, IHasModificationTime, ISoftDelete
{
    [Required]
    [StringLength(30)]
    public string ParkingCode { get; set; }

    [Required]
    [StringLength(30)]
    public string Name{ get; set; }

    [Required]
    [StringLength(255)]
    public string Location { get; set; }


    [StringLength(255)]
    public string Description{ get; set; }

    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }

    public ICollection<ParkingSpot> ParkingSpots = new List<ParkingSpot>();
    public bool IsDeleted { get; set; }

    public ParkingArea()
    {
        CreationTime = Clock.Now;
        IsDeleted = false;
    }
}
