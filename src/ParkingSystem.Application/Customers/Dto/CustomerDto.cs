
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using System;

namespace ParkingSystem.Customers.Dto

{
    [AutoMapFrom(typeof(Customer))]
    public class CustomerDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
