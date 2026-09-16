
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Authorization;
using ParkingSystem.Entities;
using ParkingSystem.Entities.Enums;
using ParkingSystem.Exceptions;
using ParkingSystem.ParkingAreas.Dto;
using ParkingSystem.ParkingSpots.Dto;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ParkingSystem.ParkingSpots;

[AbpAuthorize(PermissionNames.Pages_ParkingSpots)]
public class ParkingSpotAppService : AsyncCrudAppService<ParkingSpot,ParkingSpotDto,long,PagedParkingSpotResultRequestDto,CreateParkingSpotDto,UpdateParkingSpotDto>,IParkingSpotAppService
{
    private readonly IRepository<ParkingArea, long> _parkingAreaRepository;
    public ParkingSpotAppService(IRepository<ParkingSpot,long> repository, IRepository<ParkingArea, long> parkingAreaRepositroy) : base(repository)
    {
        _parkingAreaRepository = parkingAreaRepositroy;
    }

    protected override IQueryable<ParkingSpot> ApplySorting(IQueryable<ParkingSpot> query, PagedParkingSpotResultRequestDto input)
    {
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            return query.OrderBy(input.Sorting);
        }
        return query.OrderByDescending(x => x.Id);
    }
    protected override IQueryable<ParkingSpot> CreateFilteredQuery(PagedParkingSpotResultRequestDto input)
    {
        var query = Repository.GetAll().AsNoTracking();

        return query
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.SpotCode.Contains(input.Keyword))

        .WhereIf(input.Status.HasValue,
            x => x.Status == input.Status.Value)
        .WhereIf(input.ParkingAreaId.HasValue,
            x => x.ParkingAreaId == input.ParkingAreaId.Value);
        
    }
    public override async Task<ParkingSpotDto> GetAsync(EntityDto<long> input)
    {
        var parkingSpot = await Repository.FirstOrDefaultAsync(x => x.Id == input.Id);

        if (parkingSpot == null)
        {
            throw new ResourceNotFoundException("Parking Spot not found with id: " + input.Id);
        }

        return ObjectMapper.Map<ParkingSpotDto>(parkingSpot);
    }

    [AbpAuthorize(PermissionNames.Pages_ParkingSpots_Manager)]
    public override async Task<ParkingSpotDto> CreateAsync(CreateParkingSpotDto input)
    {
        var area = await _parkingAreaRepository.FirstOrDefaultAsync(x => x.Id == input.ParkingAreaId);
        if(area == null)
        {
          throw new ResourceNotFoundException("Parking Spot not found with id: " + input.ParkingAreaId);
        }
        if (area.ParkingMode != ParkingMode.individualSport)
        {
            throw new BusinessRuleException(
                "Parking spots can only be created for individual parking areas."
            );
        }
        var entity = ObjectMapper.Map<ParkingSpot>(input);
        var created = await Repository.InsertAsync(entity);

        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToEntityDto(created);
    }

    [AbpAuthorize(PermissionNames.Pages_ParkingSpots_Manager)]
    public override async Task<ParkingSpotDto> UpdateAsync(UpdateParkingSpotDto input)
    {
        var entity = await Repository.FirstOrDefaultAsync(input.Id);
        if (entity == null)
        {
            throw new ResourceNotFoundException("Parking Spot not found with id: " + input.Id);
        }
       
        ObjectMapper.Map(input, entity);

        await Repository.UpdateAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();
        return MapToEntityDto(entity);
    }

    
    [AbpAuthorize(PermissionNames.Pages_ParkingSpots_Manager)]
    public override async Task DeleteAsync(EntityDto<long> input)
    {
        var entity = await Repository.FirstOrDefaultAsync(input.Id);
        if (entity == null)
        {
            throw new ResourceNotFoundException("Parking Spot not found with id: " + input.Id);
        }

        await Repository.DeleteAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

    }


    [AbpAuthorize(PermissionNames.Pages_ParkingSpots_Manager)]
    public async Task ChangeStatus(ChangeStatusDto input)
    {
        var currentSpot = await Repository.FirstOrDefaultAsync(input.Id);

        if (currentSpot == null)
        {
            throw new ResourceNotFoundException(
                $"Spot not found with id: {input.Id}"
            );
        }

        // Không được can thiệp thủ công khi Spot đang thuộc luồng nghiệp vụ
        if (currentSpot.Status == ParkingSpotStatus.Reserved ||
            currentSpot.Status == ParkingSpotStatus.Occupied)
        {
            throw new CannotManipulateException(
                "Reserved or occupied parking spots cannot be manually changed."
            );
        }

        // Chỉ cho phép Manager khóa/mở Spot
        if (input.Status != ParkingSpotStatus.Available &&
            input.Status != ParkingSpotStatus.Unavailable)
        {
            throw new CannotManipulateException(
                "Parking spot can only be manually changed between Available and Unavailable."
            );
        }

        currentSpot.Status = input.Status;

        await CurrentUnitOfWork.SaveChangesAsync();
    }
}
