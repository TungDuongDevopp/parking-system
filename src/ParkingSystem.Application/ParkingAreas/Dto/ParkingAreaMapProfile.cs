

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.ParkingAreas.Dto;

public class ParkingAreaMapProfile: Profile
{
    public ParkingAreaMapProfile()
    {
        CreateMap<ParkingArea, ParkingAreaDto>();

        CreateMap<CreateParkingAreaDto, ParkingArea>();

        CreateMap<UpdateParkingAreaDto, ParkingArea>()
             .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null)); 
    }
}
