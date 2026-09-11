using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Authorization;
using ParkingSystem.Authorization.Users;
using ParkingSystem.Customers.Dto;
using ParkingSystem.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Exceptions;


namespace ParkingSystem.Customers
{
    [AbpAuthorize(PermissionNames.Pages_Customers)]
    public class CustomerAppService : AsyncCrudAppService<Customer, CustomerDto, long, PagedCustomerResultRequestDto, CreateCustomerDto, UpdateCustomerDto>, ICustomerAppService
    {
        private readonly IRepository<User, long> _userRepository;

        public CustomerAppService(IRepository<Customer, long> repository, IRepository<User, long> userRepository) : base(repository)
        {
            _userRepository = userRepository;
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

        public override async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
        {
            // Map input to entity
            var entity = ObjectMapper.Map<Customer>(input);

            // Assign current user id on server side (do not trust client)
            if (AbpSession.UserId.HasValue)
            {
                var userId = AbpSession.UserId.Value;
                // Ensure the user exists before assigning
                var exists = await _userRepository.GetAll().AnyAsync(u => u.Id == userId);
                if (!exists)
                {
                    throw new Abp.AbpException("Current user does not exist.");
                }

                entity.UserId = userId;
            }

            // Prevent creating when a customer with same phone or email already exists (including soft-deleted)
            var exists = await Repository.GetAll().IgnoreQueryFilters().AnyAsync(x => x.PhoneNumber == input.PhoneNumber || (input.Email != null && x.Email == input.Email));
            if (exists)
            {
                throw new DuplicateResourceException("A customer with the same phone number or email already exists.");
            }

            var created = await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(created);
        }

        public override async Task<CustomerDto> UpdateAsync(UpdateCustomerDto input)
        {
            // Load entity including soft-deleted ones to detect deleted state
            var entity = await Repository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new ResourceNotFoundException("Customer not found with id: "+input.Id);
            }
            if (entity.IsDeleted)
            {
                throw new CannotManipulateException("Customer cannot be manipulated in its current status");
            }

            // Ensure we preserve UserId and only update allowed fields
            var originalUserId = entity.UserId;

            // Map incoming fields onto existing entity
            ObjectMapper.Map(input, entity);

            // Preserve UserId
            entity.UserId = originalUserId;

            await Repository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            // Load entity including soft-deleted ones so we can return proper errors
            var entity = await Repository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == input.Id);
            if (entity == null)
            {
                throw new ResourceNotFoundException("Customer not found with id: " + input.Id);
            }
            if (entity.IsDeleted)
            {
                throw new CannotManipulateException("Customer cannot be manipulated in its current status");
            }

            await Repository.DeleteAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

        }
    }
}