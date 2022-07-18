using Abp.AutoMapper;
using VinaCent.Blaze.Sessions.Dto;

namespace VinaCent.Blaze.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
