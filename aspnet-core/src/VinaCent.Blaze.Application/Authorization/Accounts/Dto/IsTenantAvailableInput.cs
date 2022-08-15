using Abp.MultiTenancy;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.Authorization.Accounts.Dto
{
    public class IsTenantAvailableInput
    {
        [AppRequired]
        [AppStringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}
