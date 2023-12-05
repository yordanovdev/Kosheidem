using Abp.Application.Services;
using Kosheidem.MultiTenancy.Dto;

namespace Kosheidem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

