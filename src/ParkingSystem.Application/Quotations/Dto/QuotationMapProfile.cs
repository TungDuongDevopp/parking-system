

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Quotations.Dto;

public class QuotationMapProfile: Profile
{
    public QuotationMapProfile()
    {
        CreateMap<Quotation, QuotationDto>();
        CreateMap<UpdateQuotationDto, Quotation>()
             .ForAllMembers(opt =>
        opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<CreateQuotationDto, Quotation>();
    }
}
