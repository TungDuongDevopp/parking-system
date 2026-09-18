

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Vehicles.Dto;

public class VehicleMapProfile : Profile

{
    public VehicleMapProfile()
    {
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer != null ? s.Customer.Name : null));
        CreateMap<CreateVehicleDto, Vehicle>();
        CreateMap<UpdateVehicleDto, Vehicle>()
            .ForMember(d => d.VehicleType, opt =>
                opt.PreCondition(s => s.VehicleType.HasValue))
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
