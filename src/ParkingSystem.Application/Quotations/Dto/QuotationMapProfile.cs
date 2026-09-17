

using AutoMapper;
using ParkingSystem.Entities;

namespace ParkingSystem.Quotations.Dto;

public class QuotationMapProfile: Profile
{
    public QuotationMapProfile()
    {
        CreateMap<Quotation, QuotationDto>();
        CreateMap<UpdateQuotationDto, Quotation>()
    .ForMember(d => d.VehicleType, opt =>
        opt.PreCondition(s => s.VehicleType.HasValue))
    .ForMember(d => d.Duration, opt =>
        opt.PreCondition(s => s.Duration.HasValue))
    .ForMember(d => d.DurationUnit, opt =>
        opt.PreCondition(s => s.DurationUnit.HasValue))
    .ForMember(d => d.Price, opt =>
        opt.PreCondition(s => s.Price.HasValue));
        CreateMap<CreateQuotationDto, Quotation>();
    }
}
