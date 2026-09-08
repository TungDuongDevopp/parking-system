using Abp.Application.Services;
using ParkingSystem.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace ParkingSystem.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
