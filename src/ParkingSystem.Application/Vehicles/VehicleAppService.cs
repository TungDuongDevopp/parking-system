

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;

using Microsoft.EntityFrameworkCore;
using ParkingSystem.Authorization;
using ParkingSystem.Entities;
using ParkingSystem.Exceptions;
using ParkingSystem.Vehicles.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ParkingSystem.Vehicles;

[AbpAuthorize(PermissionNames.Pages_Vehicles)]
public class VehicleAppService : AsyncCrudAppService<Vehicle, VehicleDto, long, PagedVehicleResultRequestDto, CreateVehicleDto, UpdateVehicleDto>, IVehicleAppService
{
    private readonly IRepository<Customer,long> _customerRepository;
    public VehicleAppService(IRepository<Vehicle, long> repository,IRepository<Customer,long> customerRepository) : base(repository)
    {
        _customerRepository = customerRepository;

    }

    protected override IQueryable<Vehicle> CreateFilteredQuery(PagedVehicleResultRequestDto input)
    {
        return Repository.GetAll()
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                x.Brand.Contains(input.Keyword) ||
                x.Color.Contains(input.Keyword) ||
                x.LicensePlate.Contains(input.Keyword) ||
                x.VehicleCode.Contains(input.Keyword))

        // Status =
        .WhereIf(input.VehicleType.HasValue,
            x => x.VehicleType == input.VehicleType.Value)
        .WhereIf(input.CustomerId.HasValue,
            x=>x.CustomerId == input.CustomerId
        )
        ;
        
    }

    protected override IQueryable<Vehicle> ApplySorting(IQueryable<Vehicle> query, PagedVehicleResultRequestDto input)
    {
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            return query.OrderBy(input.Sorting);
        }
        return query.OrderByDescending(x => x.Id);
    }

    public override async Task<VehicleDto> CreateAsync(CreateVehicleDto input)
    {
        var userId = AbpSession.UserId ?? throw new ResourceNotFoundException("User not found");

        var customer = await _customerRepository
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (customer == null)
            throw new ResourceNotFoundException("Customer not found with id: " + userId);

        var entity = ObjectMapper.Map<Vehicle>(input);
        entity.CustomerId = customer.Id;

       var created = await Repository.InsertAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToEntityDto(created);
    }
    public override async Task<VehicleDto> UpdateAsync(UpdateVehicleDto input)
    {
        var entity = await Repository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == input.Id);
        if (entity == null)
        {
            throw new ResourceNotFoundException("Vehicle not found with id: " + input.Id);
        }
        if (entity.IsDeleted)
        {
            throw new CannotManipulateException("Vehicle cannot be manipulated in its current status");
        }
        ObjectMapper.Map(input, entity);

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
            throw new ResourceNotFoundException("Vehicle not found with id: " + input.Id);
        }
        if (entity.IsDeleted)
        {
            throw new CannotManipulateException("Vehicle cannot be manipulated in its current status");
        }

        await Repository.DeleteAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

    }

}
