using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using VinaCent.Blaze.DataAnnotations;

namespace VinaCent.Blaze.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class CreateTenantDto
    {
        [AppRequired]
        [AppStringLength(AbpTenantBase.MaxTenancyNameLength)]
        [AppRegex(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [AppRequired]
        [AppStringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }

        [AppRequired]
        [AppStringLength(AbpUserBase.MaxEmailAddressLength)]
        public string AdminEmailAddress { get; set; }

        [AppStringLength(AbpTenantBase.MaxConnectionStringLength)]
        public string ConnectionString { get; set; }

        public bool IsActive {get; set;}
    }
}
