

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using ParkingSystem.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Staffs.Dto;

public class ChangeStatusDto: EntityDto<long>
{
    [Required]
    [EnumDataType(typeof(StaffStatus))]
    public StaffStatus Status { get; set; }

}
