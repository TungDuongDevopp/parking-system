

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Vehicles.Dto;

public class VehicleMapProfile : Profile

{
    public VehicleMapProfile()
    {
        CreateMap<Vehicle, VehicleDto>();
        CreateMap<CreateVehicleDto, Vehicle>();
        CreateMap<UpdateVehicleDto, Vehicle>()
    .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
