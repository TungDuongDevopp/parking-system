

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Customers.Dto;

public class CustomerMapProfile : Profile
{
    public CustomerMapProfile()
    {
        CreateMap<CustomerDto, Customer>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>()
    .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
