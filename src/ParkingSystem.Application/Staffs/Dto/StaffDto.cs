

using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ParkingSystem.Entities;
using ParkingSystem.Entities.Enums;
using System;

namespace ParkingSystem.Staffs.Dto;

[AutoMapFrom (typeof(Staff))]
public class StaffDto : EntityDto<long>

{
    public string Name {  get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address {  get; set; }

    public bool IsMale { private get; set; }

    public string Gender
    {
        get
        {
            return IsMale ? "Nam" : "Nữ";
        }
    }

    public DateTime HiredDate { get; set; }

    public DateTime DateOfBirth { get; set; }

    public StaffStatus StaffStatus { get; set; }
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }
}
