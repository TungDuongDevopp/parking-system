

using Abp.Application.Services.Dto;
using ParkingSystem.Entities.Enums;


namespace ParkingSystem.Quotations.Dto;

public class PagedQuotationResultRequestDto: PagedResultRequestDto, ISortedResultRequest
{
    public string Keyword { get; set; }
    public string Sorting { get; set; }

    public VehicleType? VehicleType { get; set; }

    public int? Duration { get; set; }

    public DurationUnit? DurationUnit { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
