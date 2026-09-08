
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ParkingSystem.Entities
{
    [Table("Customers")]
    public class Customer : Entity<long>, IHasCreationTime, IHasModificationTime

    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }
        [StringLength(255)]
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime CreationTime { get; set; }
        public Customer()
        {
            CreationTime = DateTime.Now;
        }

        public long UserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
