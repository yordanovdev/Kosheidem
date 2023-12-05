using System.Threading.Tasks;
using Abp.Application.Services;
using Kosheidem.Sessions.Dto;

namespace Kosheidem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
