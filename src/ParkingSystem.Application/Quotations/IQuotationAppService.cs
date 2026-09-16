

using Abp.Application.Services;
using ParkingSystem.Quotations.Dto;

namespace ParkingSystem.Quotations;

public interface IQuotationAppService: IAsyncCrudAppService<QuotationDto,long,PagedQuotationResultRequestDto,CreateQuotationDto,UpdateQuotationDto>
{
}
