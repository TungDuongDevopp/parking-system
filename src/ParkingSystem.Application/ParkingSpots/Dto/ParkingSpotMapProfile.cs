

using AutoMapper;
using ParkingSystem.Entities;
using ParkingSystem.ParkingAreas.Dto;

namespace ParkingSystem.ParkingSpots.Dto;

public class ParkingSpotMapProfile: Profile
{
    public ParkingSpotMapProfile()
    {
        CreateMap<ParkingSpot, ParkingSpotDto>();
        CreateMap<CreateParkingSpotDto, ParkingSpot>();
        CreateMap<UpdateParkingSpotDto, ParkingSpot>()
             .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ChangeStatusDto, ParkingSpot>();
    }
}
