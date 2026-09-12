

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Staffs.Dto;

public class StaffMapProfile : Profile
{
   public StaffMapProfile()
    {
        CreateMap<StaffDto,Staff>();
        CreateMap<CreateStaffDto, Staff>();
        CreateMap<UpdateStaffDto, Staff>()
    .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
