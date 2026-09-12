
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Authorization;
using ParkingSystem.Authorization.Users;
using ParkingSystem.Entities;
using ParkingSystem.Exceptions;
using ParkingSystem.Staffs.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ParkingSystem.Staffs;

[AbpAuthorize(PermissionNames.Pages_Staffs)]
public class StaffAppService: AsyncCrudAppService<Staff, StaffDto, long, PagedStaffResultRequestDto, CreateStaffDto, UpdateStaffDto>, IStaffAppService
{
    private readonly UserManager _userManager;

    public StaffAppService(
        IRepository<Staff, long> repository,
        UserManager userManager)
        : base(repository)
    {
        _userManager = userManager;
    }

    protected override IQueryable<Staff> CreateFilteredQuery(PagedStaffResultRequestDto input)
    {
        return Repository.GetAll()
            .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                x.Name.Contains(input.Keyword) ||
                x.PhoneNumber.Contains(input.Keyword) ||
                x.Email.Contains(input.Keyword) ||
                x.Address.Contains(input.Keyword))
        
         // Ngày vào làm >= From
        .WhereIf(input.HiredDateFrom.HasValue,
            x => x.HiredDate >= input.HiredDateFrom.Value)

        // Ngày vào làm <= To
        .WhereIf(input.HiredDateTo.HasValue,
            x => x.HiredDate <= input.HiredDateTo.Value)

        // Ngày sinh >= From
        .WhereIf(input.DateOfBirthFrom.HasValue,
            x => x.DateOfBirth >= input.DateOfBirthFrom.Value)

        // Ngày sinh <= To
        .WhereIf(input.DateOfBirthTo.HasValue,
            x => x.DateOfBirth <= input.DateOfBirthTo.Value)

        // Status =
        .WhereIf(input.Status.HasValue,
            x => x.Status == input.Status.Value);
        ;
    }
    protected override IQueryable<Staff> ApplySorting(IQueryable<Staff> query, PagedStaffResultRequestDto input)
    {
        if (!string.IsNullOrEmpty(input.Sorting))
        {
            return query.OrderBy(input.Sorting);
        }
        return query.OrderByDescending(x => x.Id);
    }

    public override async Task<StaffDto> CreateAsync(CreateStaffDto input)
    {
        // 1. Kiểm tra user tồn tại
        var user = await _userManager.GetUserByIdAsync(input.UserId);
        
        if(user == null)
        {
            throw new ResourceNotFoundException("User not found with id "+ input.UserId);
        }

        // 2. User phải có role Staff
        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.Contains("Staff"))
        {
            throw new AbpException("User does not have Staff role.");
        }

        // 3. Một user chỉ được có một Staff profile
        var existingStaffByUser = await Repository
            .GetAll()
            .IgnoreQueryFilters()
            .AnyAsync(x => x.UserId == input.UserId);

        if (existingStaffByUser)
        {
            throw new DuplicateResourceException(
                "This user already has a Staff profile.");
        }

        // 4. Không trùng phone/email
        var existingStaff = await Repository
            .GetAll()
            .IgnoreQueryFilters()
            .AnyAsync(x =>
                x.PhoneNumber == input.PhoneNumber ||
                (input.Email != null && x.Email == input.Email));

        if (existingStaff)
        {
            throw new DuplicateResourceException(
                "A Staff with the same phone number or email already exists.");
        }

        // 5. Tạo Staff
        var entity = ObjectMapper.Map<Staff>(input);

        entity.UserId = user.Id;

        var created = await Repository.InsertAsync(entity);

        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToEntityDto(created);
    }

    public override async Task<StaffDto> UpdateAsync(UpdateStaffDto input)
    {
        var entity = await Repository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == input.Id);
        if (entity == null)
        {
            throw new ResourceNotFoundException("Staff not found with id: " + input.Id);
        }
        if (entity.IsDeleted)
        {
            throw new CannotManipulateException("Staff cannot be manipulated in its current status");
        }
        ObjectMapper.Map(input,entity);

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
            throw new ResourceNotFoundException("Staff not found with id: " + input.Id);
        }
        if (entity.IsDeleted)
        {
            throw new CannotManipulateException("Staff cannot be manipulated in its current status");
        }

        await Repository.DeleteAsync(entity);
        await CurrentUnitOfWork.SaveChangesAsync();

    }
}
