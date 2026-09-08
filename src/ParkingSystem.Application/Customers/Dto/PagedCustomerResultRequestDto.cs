using Abp.Application.Services.Dto;

namespace ParkingSystem.Customers.Dto
{
    public class PagedCustomerResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
    }
}
