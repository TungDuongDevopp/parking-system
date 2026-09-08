
using Abp.Application.Services;

using ParkingSystem.Customers.Dto;

namespace ParkingSystem.Customers;

public interface ICustomerAppService : IAsyncCrudAppService<CustomerDto, long, PagedCustomerResultRequestDto, CreateCustomerDto, UpdateCustomerDto>
{
}