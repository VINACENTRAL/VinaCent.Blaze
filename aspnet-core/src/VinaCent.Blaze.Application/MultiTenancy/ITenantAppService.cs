using Abp.Application.Services;
using VinaCent.Blaze.MultiTenancy.Dto;

namespace VinaCent.Blaze.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

