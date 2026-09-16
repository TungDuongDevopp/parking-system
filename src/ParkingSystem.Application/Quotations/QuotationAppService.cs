

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Authorization;
using ParkingSystem.Entities;
using ParkingSystem.Exceptions;
using ParkingSystem.ParkingAreas.Dto;
using ParkingSystem.Quotations.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;


namespace ParkingSystem.Quotations;

[AbpAuthorize(PermissionNames.Pages_Quotations)]
public class QuotationAppService: AsyncCrudAppService<Quotation,QuotationDto,long,PagedQuotationResultRequestDto,CreateQuotationDto,UpdateQuotationDto>, IQuotationAppService
{
    public QuotationAppService(IRepository<Quotation,long> repository) : base(repository)
    {

    }

    protected override IQueryable<Quotation> ApplySorting(IQueryable<Quotation> query, PagedQuotationResultRequestDto input)
    {
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            return query.OrderBy(input.Sorting);
        }
        return query.OrderByDescending(x => x.Id);
    }
    protected override IQueryable<Quotation> CreateFilteredQuery(PagedQuotationResultRequestDto input)
    {
        var query = Repository.GetAll().AsNoTracking();

        return query
        .WhereIf(input.VehicleType.HasValue,
            x => x.VehicleType == input.VehicleType.Value)
        .WhereIf(input.Duration.HasValue,
            x => x.Duration == input.Duration.Value)
        .WhereIf(input.DurationUnit.HasValue,
            x => x.DurationUnit == input.DurationUnit.Value)
        .WhereIf(input.MinPrice.HasValue,
            x => x.Price >= input.MinPrice.Value)
        .WhereIf(input.MaxPrice.HasValue,
            x => x.Price <= input.MaxPrice.Value);
    }

    public override async Task<QuotationDto>  UpdateAsync(UpdateQuotationDto input)
    {
        var entity = await Repository.FirstOrDefaultAsync(input.Id);

        if(entity == null)
        {
            throw new ResourceNotFoundException("Quotation not found with id: " + input.Id);
        }
        var exists = await Repository.GetAll().AnyAsync(
         x => x.Id != input.Id
               && x.Duration == input.Duration
               && x.DurationUnit == input.DurationUnit
               && x.VehicleType == input.VehicleType
        );

        if (exists)
        {
            throw new DuplicateResourceException("Quotation already exists.");
        }
        
        ObjectMapper.Map(input, entity);

        await Repository.UpdateAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();
        return MapToEntityDto(entity);
    }

    public override async Task<QuotationDto> CreateAsync(CreateQuotationDto input)
    {
        var exists = await Repository.GetAll().AnyAsync(
             x => x.Duration == input.Duration
            && x.DurationUnit == input.DurationUnit
            && x.VehicleType == input.VehicleType
 );

        if (exists)
        {
            throw new DuplicateResourceException("Quotation already exists.");
        }
        var entity = ObjectMapper.Map<Quotation>(input);
        var created = await Repository.InsertAsync(entity);

        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToEntityDto(created);
    }

    public override async Task<QuotationDto> GetAsync(EntityDto<long> input)
    {
        var quotation = await Repository.FirstOrDefaultAsync(x => x.Id == input.Id);

        if (quotation == null)
        {
            throw new ResourceNotFoundException("Quotation not found with id: " + input.Id);
        }

        return ObjectMapper.Map<QuotationDto>(quotation);
    }

    public override async Task DeleteAsync(EntityDto<long> input)
    {
        var entity = await Repository.FirstOrDefaultAsync(input.Id);
        if (entity == null)
        {
            throw new ResourceNotFoundException("Quotation not found with id: " + input.Id);
        }

        await Repository.DeleteAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

    }
}
