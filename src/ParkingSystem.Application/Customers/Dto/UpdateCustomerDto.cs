

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Customers.Dto;

[AutoMap(typeof(Customer))]
public class UpdateCustomerDto: EntityDto<long>
{

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(20)]
    [Phone]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
}
