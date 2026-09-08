using Abp.Application.Services;
using ParkingSystem.Sessions.Dto;
using System.Threading.Tasks;

namespace ParkingSystem.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
