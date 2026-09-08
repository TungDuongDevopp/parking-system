using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using ParkingSystem.Authorization;
using ParkingSystem.Customers.Dto;
using ParkingSystem.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;


namespace ParkingSystem.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, long, PagedCustomerResultRequestDto, CreateCustomerDto, UpdateCustomerDto>, ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, long> repository) : base(repository)
        {
        }
        protected override IQueryable<Customer> CreateFilteredQuery(PagedCustomerResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Name.Contains(input.Keyword) ||
                    x.PhoneNumber.Contains(input.Keyword) ||
                    x.Email.Contains(input.Keyword));
        }
        protected override IQueryable<Customer> ApplySorting(IQueryable<Customer> query, PagedCustomerResultRequestDto input)
        {
            if (!string.IsNullOrEmpty(input.Sorting))
            {
                return query.OrderBy(input.Sorting);
            }
            return query.OrderByDescending(x => x.Id);
        }
      
    }
}