

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;
using System;

namespace ParkingSystem.Staffs.Dto;

public class StaffDto : EntityDto<long>

{
    public string Name {  get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Address {  get; set; }

    public bool Gender { get; set; }

    public string GenderName => Gender ? "Nam" : "Nữ";

    public DateTime HiredDate { get; set; }

    public DateTime DateOfBirth { get; set; }

    public StaffStatus Status { get; set; }
    public string StatusName => Status.ToString();
    public DateTime CreationTime { get; set; }
    public bool IsDeleted { get; set; }
}
